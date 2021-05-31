using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public class Transfer : Transaction, ITransfer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IColumbarium _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;

        public Transfer(
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

        public void SetTransfer(string AF)
        {
            SetTransaction(AF);
        }

        public float GetPrice(int itemId)
        {
            _item.SetItem(itemId);
            return _item.GetPrice();
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        public bool AllowNicheDeceasePairing(int nicheId, int applicantId)
        {
            _niche.SetNiche(nicheId);

            if (_niche.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByNicheId(nicheId);
                foreach (var deceased in deceaseds)
                {
                    var applicantDeceased = _applicantDeceased.GetApplicantDeceased(applicantId, deceased.Id);
                    if (applicantDeceased != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        public bool Create(ColumbariumTransactionDto columbariumTransactionDto)
        {
            _niche.SetNiche(columbariumTransactionDto.NicheDtoId);
            if (_niche.GetApplicantId() == columbariumTransactionDto.ApplicantDtoId)
                return false;

            if (!AllowNicheDeceasePairing(columbariumTransactionDto.NicheDtoId, columbariumTransactionDto.ApplicantDtoId))
                return false;

            if (!SetTransactionDeceasedIdBasedOnNiche(columbariumTransactionDto, columbariumTransactionDto.NicheDtoId))
                return false;

            columbariumTransactionDto.TransferredColumbariumTransactionDtoAF = _tracking.GetLatestFirstTransactionByNicheId(columbariumTransactionDto.NicheDtoId).ColumbariumTransactionAF;

            GetTransaction(columbariumTransactionDto.TransferredColumbariumTransactionDtoAF).DeleteDate = System.DateTime.Now;

            _tracking.Remove(columbariumTransactionDto.NicheDtoId, columbariumTransactionDto.TransferredColumbariumTransactionDtoAF);

            NewNumber(columbariumTransactionDto.ColumbariumItemDtoId);

            SummaryItem(columbariumTransactionDto);

            if (CreateNewTransaction(columbariumTransactionDto))
            {
                _niche.SetApplicant(columbariumTransactionDto.ApplicantDtoId);

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
            if (_invoice.GetInvoicesByAF(columbariumTransactionDto.AF).Any() && columbariumTransactionDto.Price <
                _invoice.GetInvoicesByAF(columbariumTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            SummaryItem(columbariumTransactionDto);

            UpdateTransaction(columbariumTransactionDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.NicheId, _transaction.AF))
                return false;

            _niche.SetNiche(_transaction.NicheId);

            _niche.RemoveApplicant();

            _niche.SetHasDeceased(false);

            var deceaseds = _deceased.GetDeceasedsByNicheId(_transaction.NicheId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveNiche();
            }

            _tracking.Remove(_transaction.NicheId, _transaction.AF);


            var previousTransaction = GetTransactionExclusive(_transaction.TransferredColumbariumTransactionAF);

            _niche.SetNiche(previousTransaction.NicheId);

            _niche.SetApplicant(previousTransaction.ApplicantId);

            if (previousTransaction.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)previousTransaction.Deceased1Id);

                if (_deceased.GetNiche() != null && _deceased.GetNiche().Id != _transaction.NicheId)
                    return false;

                _deceased.SetNiche(previousTransaction.NicheId);

                _niche.SetHasDeceased(true);
            }

            if (previousTransaction.Deceased2Id != null)
            {
                _deceased.SetDeceased((int)previousTransaction.Deceased2Id);

                if (_deceased.GetNiche() != null && _deceased.GetNiche().Id != _transaction.NicheId)
                    return false;

                _deceased.SetNiche(previousTransaction.NicheId);

                _niche.SetHasDeceased(true);
            }

            DeleteTransaction();

            previousTransaction.DeleteDate = null;

            _tracking.Add(previousTransaction.NicheId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.Deceased1Id, previousTransaction.Deceased2Id);


            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(ColumbariumTransactionDto trx)
        {
            _applicant.SetApplicant(trx.ApplicantDtoId);

            trx.SummaryItem = "AF: " + trx.AF == null ? _AFnumber : trx.AF + "<BR/>" +
                Resources.Mix.Niche + ": " + _applicant.GetApplicant((int)trx.TransferredApplicantDtoId).Name + "<BR/>" +
                Resources.Mix.TransferTo + "<BR/>" +
                Resources.Mix.Niche + ": " + _applicant.GetApplicant().Name + "<BR/>" +
                Resources.Mix.Remark + ": " + trx.Remark;
        }
    }
}