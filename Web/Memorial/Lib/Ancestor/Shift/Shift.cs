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
        private readonly IAncestorApplicantDeceaseds _ancestorApplicantDeceaseds;

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
            IAncestorApplicantDeceaseds ancestorApplicantDeceaseds
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
            _ancestorApplicantDeceaseds = ancestorApplicantDeceaseds;
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
            if (_ancestor.HasDeceased())
            {
                foreach (var deceased in deceaseds)
                {
                    _deceased.SetDeceased(deceased.Id);
                    _deceased.SetAncestor(newAncestorId);
                }
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


            _ancestor.SetAncestor(ancestorTransactionDto.AncestorId);
            if (!SetDeceasedIdBasedOnAncestorLastTransaction(ancestorTransactionDto))
                return false;

            NewNumber(ancestorTransactionDto.AncestorItemId);

            if (CreateNewTransaction(ancestorTransactionDto))
            {
                ShiftAncestorApplicantDeceaseds(ancestorTransactionDto.AncestorId, (int)ancestorTransactionDto.ShiftedAncestorId, ancestorTransactionDto.ApplicantId);

                _tracking.Add(ancestorTransactionDto.AncestorId, _AFnumber);

                _tracking.Add((int)ancestorTransactionDto.ShiftedAncestorId, _AFnumber, ancestorTransactionDto.ApplicantId, ancestorTransactionDto.DeceasedId);

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
                _tracking.Remove((int)ancestorTransactionInDb.ShiftedAncestorId, ancestorTransactionDto.AF);

                _tracking.Add((int)ancestorTransactionDto.ShiftedAncestorId, ancestorTransactionDto.AF, ancestorTransactionDto.ApplicantId, ancestorTransactionDto.DeceasedId);

                ShiftAncestorApplicantDeceaseds((int)ancestorTransactionInDb.ShiftedAncestorId, (int)ancestorTransactionDto.ShiftedAncestorId, ancestorTransactionDto.ApplicantId);
            }

            UpdateTransaction(ancestorTransactionDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction((int)_transaction.ShiftedAncestorId, _transaction.AF))
                return false;

            DeleteTransaction();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _ancestorApplicantDeceaseds.RollbackAncestorApplicantDeceaseds(_transaction.AF, (int)_transaction.ShiftedAncestorId);

            _unitOfWork.Complete();

            return true;
        }

    }
}