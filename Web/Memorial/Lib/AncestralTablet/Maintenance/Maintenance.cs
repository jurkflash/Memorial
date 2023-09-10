using Memorial.Core;
using System;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Lib.AncestralTablet
{
    public class Maintenance : Transaction, IMaintenance
    {
        private const string _systemCode = "Maintenance";
        private readonly IUnitOfWork _unitOfWork;

        public Maintenance(
            IUnitOfWork unitOfWork,
            IItem item,
            IAncestralTablet ancestralTablet,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            ) :
            base(
                unitOfWork,
                item,
                ancestralTablet,
                applicant,
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _ancestralTablet = ancestralTablet;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public bool ChangeAncestralTablet(int oldAncestralTabletId, int newAncestralTabletId)
        {
            if (!ChangeAncestralTablet(_systemCode, oldAncestralTabletId, newAncestralTabletId))
                return false;

            return true;
        }

        public float GetAmount(int itemId, DateTime from, DateTime to)
        {
            var item = _item.GetById(itemId);
            
            if (from > to)
                return -1;

            var total = (((to.Year - from.Year) * 12) + to.Month - from.Month) * _item.GetPrice(item);

            return total;
        }

        public bool Add(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction)
        {
            ancestralTabletTransaction.AF = _number.GetNewAF(ancestralTabletTransaction.AncestralTabletItemId, System.DateTime.Now.Year);
            SummaryItem(ancestralTabletTransaction);

            _unitOfWork.AncestralTabletTransactions.Add(ancestralTabletTransaction);
            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.AncestralTabletTransaction ancestralTabletTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveAncestralTabletAF(ancestralTabletTransaction.AF).ToList();

            if (invoices.Any() && ancestralTabletTransaction.Price < invoices.Max(i => i.Amount))
                return false;

            SummaryItem(ancestralTabletTransaction);

            var ancestralTabletTransactionInDb = GetByAF(ancestralTabletTransaction.AF);
            ancestralTabletTransactionInDb.Price = ancestralTabletTransaction.Price;
            ancestralTabletTransactionInDb.SummaryItem = ancestralTabletTransaction.SummaryItem;
            ancestralTabletTransactionInDb.Remark = ancestralTabletTransaction.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            if (_unitOfWork.Invoices.GetByActiveAncestralTabletAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByAncestralTabletAF(AF).Any())
                return false;

            var transactionInDb = _unitOfWork.AncestralTabletTransactions.GetByAF(AF);
            _unitOfWork.AncestralTabletTransactions.Remove(transactionInDb);
            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction)
        {
            var ancestralTablet = _ancestralTablet.GetById(ancestralTabletTransaction.AncestralTabletId);

            ancestralTabletTransaction.SummaryItem = "AF: " + ancestralTabletTransaction.AF + "<BR/>" +
                Resources.Mix.AncestralTablet + ": " + ancestralTablet.Name + "<BR/>" +
                Resources.Mix.From + ": " + ancestralTabletTransaction.FromDate.Value.ToString("yyyy-MMM-dd HH:mm") + " " +
                Resources.Mix.To + ": " + ancestralTabletTransaction.ToDate.Value.ToString("yyyy-MMM-dd HH:mm") + "<BR/>" +
                Resources.Mix.Remark + ": " + ancestralTabletTransaction.Remark;
        }
    }
}