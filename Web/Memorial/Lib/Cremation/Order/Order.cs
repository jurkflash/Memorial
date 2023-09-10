using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;

namespace Memorial.Lib.Cremation
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            ICremation cremation,
            IApplicant applicant,
            INumber number
            ) : 
            base(
                unitOfWork, 
                item,
                cremation, 
                applicant, 
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _cremation = cremation;
            _applicant = applicant;
            _number = number;
        }

        public bool Add(Core.Domain.CremationTransaction cremationTransaction)
        {
            cremationTransaction.AF = _number.GetNewAF(cremationTransaction.CremationItemId, System.DateTime.Now.Year);
            SummaryItem(cremationTransaction);

            _unitOfWork.CremationTransactions.Add(cremationTransaction);
            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.CremationTransaction cremationTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveSpaceAF(cremationTransaction.AF).ToList();

            if (invoices.Any() && cremationTransaction.Price < invoices.Max(i => i.Amount))
                return false;

            SummaryItem(cremationTransaction);

            var cremationTransactionInDb = GetByAF(cremationTransaction.AF);
            cremationTransactionInDb.Price = cremationTransaction.Price;
            cremationTransactionInDb.SummaryItem = cremationTransaction.SummaryItem;
            cremationTransactionInDb.Remark = cremationTransaction.Remark;
            cremationTransactionInDb.FuneralCompanyId = cremationTransaction.FuneralCompanyId;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            if (_unitOfWork.Invoices.GetByActiveCremationAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByCremationAF(AF).Any())
                return false;

            var transactionInDb = _unitOfWork.CremationTransactions.GetByAF(AF);
            _unitOfWork.CremationTransactions.Remove(transactionInDb);
            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(Core.Domain.CremationTransaction cremationTransaction)
        {
            cremationTransaction.SummaryItem = "AF: " + cremationTransaction.AF + "<BR/>" +
                Resources.Mix.CremateDate + ": " + cremationTransaction.CremateDate.ToString("yyyy-MMM-dd HH:mm") + "<BR/>" +
                Resources.Mix.Remark + ": " + cremationTransaction.Remark;
        }

    }
}