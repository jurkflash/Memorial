using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public class Shift : Transaction, IShift
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IAncestor _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;
        private readonly IMaintenance _maintenance;

        public Shift(
            IUnitOfWork unitOfWork,
            IItem item,
            IAncestor ancestor,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IAncestor invoice,
            IPayment payment,
            ITracking tracking,
            IMaintenance maintenance
            ) :
            base(
                unitOfWork,
                item,
                ancestor,
                applicant,
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _ancestor = ancestor;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _invoice = invoice;
            _payment = payment;
            _tracking = tracking;
            _maintenance = maintenance;
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

        private bool ShiftAncestorApplicantDeceaseds(int oldQuadranlgeId, int newAncestorId, int newApplicantId)
        {
            _ancestor.SetAncestor(oldQuadranlgeId);

            var deceaseds = _deceased.GetDeceasedsByAncestorId(oldQuadranlgeId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.SetAncestor(newAncestorId);
            }

            if (deceaseds.Any())
            {
                _ancestor.SetHasDeceased(false);
            }

            _ancestor.RemoveApplicant();

            _ancestor.SetAncestor(newAncestorId);
            _ancestor.SetApplicant(newApplicantId);
            _ancestor.SetHasDeceased(deceaseds.Any());

            return true;
        }

        public bool Create(AncestorTransactionDto ancestorTransactionDto)
        {
            _ancestor.SetAncestor((int)ancestorTransactionDto.ShiftedAncestorId);
            if (_ancestor.HasApplicant())
                return false;

            if (!SetTransactionDeceasedIdBasedOnAncestor(ancestorTransactionDto, (int)ancestorTransactionDto.ShiftedAncestorId))
                return false;

            ancestorTransactionDto.ShiftedAncestorTransactionAF = _tracking.GetLatestFirstTransactionByAncestorId((int)ancestorTransactionDto.ShiftedAncestorId).AncestorTransactionAF;

            GetTransaction(ancestorTransactionDto.ShiftedAncestorTransactionAF).DeleteDate = System.DateTime.Now;

            _tracking.Remove((int)ancestorTransactionDto.ShiftedAncestorId, ancestorTransactionDto.ShiftedAncestorTransactionAF);

            NewNumber(ancestorTransactionDto.AncestorItemId);

            if (CreateNewTransaction(ancestorTransactionDto))
            {
                ShiftAncestorApplicantDeceaseds(ancestorTransactionDto.AncestorId, (int)ancestorTransactionDto.ShiftedAncestorId, ancestorTransactionDto.ApplicantId);

                _maintenance.ChangeAncestor((int)ancestorTransactionDto.ShiftedAncestorId, ancestorTransactionDto.AncestorId);

                _tracking.Add(ancestorTransactionDto.AncestorId, _AFnumber, ancestorTransactionDto.ApplicantId, ancestorTransactionDto.DeceasedId);

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Update(AncestorTransactionDto ancestorTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(ancestorTransactionDto.AF).Any() && ancestorTransactionDto.Price <
                _invoice.GetInvoicesByAF(ancestorTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var ancestorTransactionInDb = GetTransaction(ancestorTransactionDto.AF);

            if(ancestorTransactionInDb.ShiftedAncestorId != ancestorTransactionDto.ShiftedAncestorId)
            {
                if (!SetTransactionDeceasedIdBasedOnAncestor(ancestorTransactionDto, ancestorTransactionInDb.AncestorId))
                    return false;

                _tracking.Remove(ancestorTransactionInDb.AncestorId, ancestorTransactionDto.AF);

                _tracking.Add(ancestorTransactionDto.AncestorId, ancestorTransactionDto.AF, ancestorTransactionDto.ApplicantId, ancestorTransactionDto.DeceasedId);

                ShiftAncestorApplicantDeceaseds(ancestorTransactionInDb.AncestorId, ancestorTransactionDto.AncestorId, ancestorTransactionDto.ApplicantId);

                _maintenance.ChangeAncestor(ancestorTransactionInDb.AncestorId, ancestorTransactionDto.AncestorId);

                UpdateTransaction(ancestorTransactionDto);

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Delete()
        {
            if (GetTransactionsByShiftedAncestorTransactionAF(_transaction.AF) != null)
                return false;

            if (!_tracking.IsLatestTransaction((int)_transaction.ShiftedAncestorId, _transaction.AF))
                return false;

            _ancestor.SetAncestor((int)_transaction.ShiftedAncestorId);
            if (_ancestor.HasApplicant())
                return false;

            DeleteTransaction();


            _ancestor.SetAncestor(_transaction.AncestorId);

            _ancestor.RemoveApplicant();

            _ancestor.SetHasDeceased(false);

            var deceaseds = _deceased.GetDeceasedsByAncestorId(_transaction.AncestorId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveNiche();
            }

            _tracking.Remove(_transaction.AncestorId, _transaction.AF);


            var previousTransaction = GetTransactionExclusive(_transaction.ShiftedAncestorTransactionAF);

            _ancestor.SetAncestor(previousTransaction.AncestorId);

            _ancestor.SetApplicant(previousTransaction.ApplicantId);

            if (previousTransaction.DeceasedId != null)
            {
                _deceased.SetDeceased((int)previousTransaction.DeceasedId);

                if (_deceased.GetAncestor() != null && _deceased.GetAncestor().Id != _transaction.AncestorId)
                    return false;

                _deceased.SetAncestor(previousTransaction.AncestorId);

                _ancestor.SetHasDeceased(true);
            }

            previousTransaction.DeleteDate = null;

            _tracking.Add(previousTransaction.AncestorId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.DeceasedId);

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _maintenance.ChangeAncestor(_transaction.AncestorId, previousTransaction.AncestorId);

            _unitOfWork.Complete();

            return true;
        }

    }
}