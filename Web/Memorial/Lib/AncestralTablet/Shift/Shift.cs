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

        public bool Create(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            _ancestor.SetAncestor((int)ancestralTabletTransactionDto.ShiftedAncestorId);
            if (_ancestor.HasApplicant())
                return false;

            if (!SetTransactionDeceasedIdBasedOnAncestor(ancestralTabletTransactionDto, (int)ancestralTabletTransactionDto.ShiftedAncestorId))
                return false;

            ancestralTabletTransactionDto.ShiftedAncestralTabletTransactionAF = _tracking.GetLatestFirstTransactionByAncestorId((int)ancestralTabletTransactionDto.ShiftedAncestorId).AncestralTabletTransactionAF;

            GetTransaction(ancestralTabletTransactionDto.ShiftedAncestralTabletTransactionAF).DeleteDate = System.DateTime.Now;

            _tracking.Remove((int)ancestralTabletTransactionDto.ShiftedAncestorId, ancestralTabletTransactionDto.ShiftedAncestralTabletTransactionAF);

            NewNumber(ancestralTabletTransactionDto.AncestralTabletItemId);

            if (CreateNewTransaction(ancestralTabletTransactionDto))
            {
                ShiftAncestorApplicantDeceaseds(ancestralTabletTransactionDto.AncestorId, (int)ancestralTabletTransactionDto.ShiftedAncestorId, ancestralTabletTransactionDto.ApplicantId);

                _maintenance.ChangeAncestor((int)ancestralTabletTransactionDto.ShiftedAncestorId, ancestralTabletTransactionDto.AncestorId);

                _tracking.Add(ancestralTabletTransactionDto.AncestorId, _AFnumber, ancestralTabletTransactionDto.ApplicantId, ancestralTabletTransactionDto.DeceasedId);

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Update(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(ancestralTabletTransactionDto.AF).Any() && ancestralTabletTransactionDto.Price <
                _invoice.GetInvoicesByAF(ancestralTabletTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var ancestralTabletTransactionInDb = GetTransaction(ancestralTabletTransactionDto.AF);

            if(ancestralTabletTransactionInDb.ShiftedAncestorId != ancestralTabletTransactionDto.ShiftedAncestorId)
            {
                if (!SetTransactionDeceasedIdBasedOnAncestor(ancestralTabletTransactionDto, ancestralTabletTransactionInDb.AncestorId))
                    return false;

                _tracking.Remove(ancestralTabletTransactionInDb.AncestorId, ancestralTabletTransactionDto.AF);

                _tracking.Add(ancestralTabletTransactionDto.AncestorId, ancestralTabletTransactionDto.AF, ancestralTabletTransactionDto.ApplicantId, ancestralTabletTransactionDto.DeceasedId);

                ShiftAncestorApplicantDeceaseds(ancestralTabletTransactionInDb.AncestorId, ancestralTabletTransactionDto.AncestorId, ancestralTabletTransactionDto.ApplicantId);

                _maintenance.ChangeAncestor(ancestralTabletTransactionInDb.AncestorId, ancestralTabletTransactionDto.AncestorId);

                UpdateTransaction(ancestralTabletTransactionDto);

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Delete()
        {
            if (GetTransactionsByShiftedAncestralTabletTransactionAF(_transaction.AF) != null)
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


            var previousTransaction = GetTransactionExclusive(_transaction.ShiftedAncestralTabletTransactionAF);

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