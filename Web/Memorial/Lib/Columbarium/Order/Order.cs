using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IColumbarium _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            INiche niche,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IColumbarium invoice,
            IPayment payment,
            ITracking tracking
            ) : 
            base(
                unitOfWork, 
                item, 
                niche, 
                applicant, 
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _niche = niche;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _invoice = invoice;
            _payment = payment;
            _tracking = tracking;
        }

        public void SetOrder(string AF)
        {
            SetTransaction(AF);
        }

        private void SetDeceased(int id)
        {
            _deceased.SetDeceased(id);
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        public bool Create(ColumbariumTransactionDto columbariumTransactionDto)
        {
            if (columbariumTransactionDto.DeceasedDto1Id != null)
            {
                SetDeceased((int)columbariumTransactionDto.DeceasedDto1Id);
                if (_deceased.GetNiche() != null)
                    return false;
            }

            NewNumber(columbariumTransactionDto.ColumbariumItemDtoId);

            if (CreateNewTransaction(columbariumTransactionDto))
            {
                _niche.SetNiche(columbariumTransactionDto.NicheDtoId);
                _niche.SetApplicant(columbariumTransactionDto.ApplicantDtoId);

                if (columbariumTransactionDto.DeceasedDto1Id != null)
                {
                    SetDeceased((int)columbariumTransactionDto.DeceasedDto1Id);
                    if (_deceased.SetNiche(columbariumTransactionDto.NicheDtoId))
                    {
                        _niche.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                if (columbariumTransactionDto.DeceasedDto2Id != null)
                {
                    SetDeceased((int)columbariumTransactionDto.DeceasedDto2Id);
                    if (_deceased.SetNiche(columbariumTransactionDto.NicheDtoId))
                    {
                        _niche.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                _tracking.Add(columbariumTransactionDto.NicheDtoId, _AFnumber, columbariumTransactionDto.ApplicantDtoId, columbariumTransactionDto.DeceasedDto1Id, columbariumTransactionDto.DeceasedDto2Id);

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(ColumbariumTransactionDto columbariumTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(columbariumTransactionDto.AF).Any() && columbariumTransactionDto.Price + (float)columbariumTransactionDto.Maintenance + (float)columbariumTransactionDto.LifeTimeMaintenance < 
                _invoice.GetInvoicesByAF(columbariumTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var columbariumTransactionInDb = GetTransaction(columbariumTransactionDto.AF);

            var deceased1InDb = columbariumTransactionInDb.Deceased1Id;

            var deceased2InDb = columbariumTransactionInDb.Deceased2Id;

            if (UpdateTransaction(columbariumTransactionDto))
            {
                _niche.SetNiche(columbariumTransactionDto.NicheDtoId);

                NicheApplicantDeceaseds(columbariumTransactionDto.DeceasedDto1Id, deceased1InDb);

                NicheApplicantDeceaseds(columbariumTransactionDto.DeceasedDto2Id, deceased2InDb);

                if (columbariumTransactionDto.DeceasedDto1Id == null && columbariumTransactionDto.DeceasedDto2Id == null)
                    _niche.SetHasDeceased(false);

                _tracking.Change(columbariumTransactionDto.NicheDtoId, columbariumTransactionDto.AF, columbariumTransactionDto.ApplicantDtoId, columbariumTransactionDto.DeceasedDto1Id, columbariumTransactionDto.DeceasedDto2Id);

                _unitOfWork.Complete();
            }

            return true;
        }

        private bool NicheApplicantDeceaseds(int? deceasedId, int? dbDeceasedId)
        {
            if (deceasedId != dbDeceasedId)
            {
                if (deceasedId == null)
                {
                    _deceased.SetDeceased((int)dbDeceasedId);
                    _deceased.RemoveNiche();
                }
                else
                {
                    _deceased.SetDeceased((int)deceasedId);
                    if (_deceased.GetNiche() != null && _deceased.GetNiche().Id != _niche.GetNiche().Id)
                    {
                        return false;
                    }
                    else
                    {
                        _deceased.SetNiche(_niche.GetNiche().Id);
                        _niche.SetHasDeceased(true);
                    }
                }
            }

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.NicheId, _transaction.AF))
                return false;

            DeleteAllTransactionWithSameNicheId();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();


            _niche.SetNiche(_transaction.NicheId);

            var deceaseds = _deceased.GetDeceasedsByNicheId(_transaction.NicheId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveNiche();
            }

            _niche.SetHasDeceased(false);

            _niche.RemoveApplicant();

            _tracking.Delete(_transaction.AF);

            _unitOfWork.Complete();

            return true;
        }

    }
}