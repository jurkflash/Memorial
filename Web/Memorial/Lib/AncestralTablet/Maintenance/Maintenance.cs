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
    public class Maintenance : Transaction, IMaintenance
    {
        private const string _systemCode = "Maintenance";
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IAncestor _invoice;
        private readonly IPayment _payment;

        public Maintenance(
            IUnitOfWork unitOfWork,
            IItem item,
            IAncestor ancestor,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IAncestor invoice,
            IPayment payment
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
        }

        public void SetMaintenance(string AF)
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

        public bool Create(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            NewNumber(ancestralTabletTransactionDto.AncestorItemId);

            if (CreateNewTransaction(ancestralTabletTransactionDto))
            {
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

            UpdateTransaction(ancestralTabletTransactionDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete()
        {
            DeleteTransaction();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

        public bool ChangeAncestor(int oldAncestorId, int newAncestorId)
        {
            if (!ChangeAncestor(_systemCode, oldAncestorId, newAncestorId))
                return false;

            return true;
        }

        public float GetAmount(int itemId, DateTime from, DateTime to)
        {
            _item.SetItem(itemId);
            
            if (from > to)
                return -1;

            var total = (((to.Year - from.Year) * 12) + to.Month - from.Month) * _item.GetPrice();

            return total;
        }
    }
}