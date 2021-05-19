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
    public class Shift : Transaction, IShift
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IColumbarium _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;
        private readonly IManage _manage;
        private readonly IPhoto _photo;

        public Shift(
            IUnitOfWork unitOfWork,
            IItem item,
            INiche niche,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IColumbarium invoice,
            IPayment payment,
            ITracking tracking,
            IManage manage,
            IPhoto photo
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
            _manage = manage;
            _photo = photo;
        }

        public void SetShift(string AF)
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

        private bool ShiftNicheApplicantDeceaseds(int oldQuadranlgeId, int newNicheId, int newApplicantId)
        {
            _niche.SetNiche(oldQuadranlgeId);

            var deceaseds = _deceased.GetDeceasedsByNicheId(oldQuadranlgeId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.SetNiche(newNicheId);
            }

            if (deceaseds.Any())
            {
                _niche.SetHasDeceased(false);
            }

            _niche.RemoveApplicant();

            _niche.SetNiche(newNicheId);
            _niche.SetApplicant(newApplicantId);
            _niche.SetHasDeceased(deceaseds.Any());

            return true;
        }

        public bool Create(ColumbariumTransactionDto columbariumTransactionDto)
        {
            _niche.SetNiche(columbariumTransactionDto.NicheId);
            if (_niche.HasApplicant())
                return false;

            if(!SetTransactionDeceasedIdBasedOnNiche(columbariumTransactionDto, (int)columbariumTransactionDto.ShiftedNicheId))
                return false;

            columbariumTransactionDto.ShiftedColumbariumTransactionAF = _tracking.GetLatestFirstTransactionByNicheId((int)columbariumTransactionDto.ShiftedNicheId).ColumbariumTransactionAF;

            GetTransaction(columbariumTransactionDto.ShiftedColumbariumTransactionAF).DeleteDate = System.DateTime.Now;

            _tracking.Remove((int)columbariumTransactionDto.ShiftedNicheId, columbariumTransactionDto.ShiftedColumbariumTransactionAF);

            NewNumber(columbariumTransactionDto.ColumbariumItemId);

            if (CreateNewTransaction(columbariumTransactionDto))
            {
                ShiftNicheApplicantDeceaseds((int)columbariumTransactionDto.ShiftedNicheId, columbariumTransactionDto.NicheId, columbariumTransactionDto.ApplicantId);

                _manage.ChangeNiche((int)columbariumTransactionDto.ShiftedNicheId, columbariumTransactionDto.NicheId);

                _photo.ChangeNiche((int)columbariumTransactionDto.ShiftedNicheId, columbariumTransactionDto.NicheId);
                
                _tracking.Add(columbariumTransactionDto.NicheId, _AFnumber, columbariumTransactionDto.ApplicantId, columbariumTransactionDto.Deceased1Id, columbariumTransactionDto.Deceased2Id);

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

            var columbariumTransactionInDb = GetTransaction(columbariumTransactionDto.AF);

            if (columbariumTransactionInDb.NicheId != columbariumTransactionDto.NicheId)
            {
                if (!SetTransactionDeceasedIdBasedOnNiche(columbariumTransactionDto, columbariumTransactionInDb.NicheId))
                    return false;

                _tracking.Remove(columbariumTransactionInDb.NicheId, columbariumTransactionDto.AF);

                _tracking.Add(columbariumTransactionDto.NicheId, columbariumTransactionDto.AF, columbariumTransactionDto.ApplicantId, columbariumTransactionDto.Deceased1Id, columbariumTransactionDto.Deceased2Id);

                ShiftNicheApplicantDeceaseds(columbariumTransactionInDb.NicheId, columbariumTransactionDto.NicheId, columbariumTransactionDto.ApplicantId);

                _manage.ChangeNiche(columbariumTransactionInDb.NicheId, columbariumTransactionDto.NicheId);

                _photo.ChangeNiche(columbariumTransactionInDb.NicheId, columbariumTransactionDto.NicheId);

                UpdateTransaction(columbariumTransactionDto);

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Delete()
        {
            if(GetTransactionsByShiftedColumbariumTransactionAF(_transaction.AF) != null)
                return false;

            if (!_tracking.IsLatestTransaction(_transaction.NicheId, _transaction.AF))
                return false;

            _niche.SetNiche((int)_transaction.ShiftedNicheId);
            if (_niche.HasApplicant())
                return false;

            DeleteTransaction();


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


            var previousTransaction = GetTransactionExclusive(_transaction.ShiftedColumbariumTransactionAF);

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

            previousTransaction.DeleteDate = null;

            _tracking.Add(previousTransaction.NicheId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.Deceased1Id, previousTransaction.Deceased2Id);

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _manage.ChangeNiche(_transaction.NicheId, previousTransaction.NicheId);

            _photo.ChangeNiche(_transaction.NicheId, previousTransaction.NicheId);

            _unitOfWork.Complete();
            
            return true;
        }

    }
}