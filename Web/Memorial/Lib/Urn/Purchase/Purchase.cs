using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;

namespace Memorial.Lib.Urn
{
    public class Purchase : Transaction, IPurchase
    {
        private readonly IUnitOfWork _unitOfWork;

        public Purchase(
            IUnitOfWork unitOfWork,
            IItem item,
            IUrn urn,
            IApplicant applicant,
            INumber number
            ) : 
            base(
                unitOfWork, 
                item, 
                urn, 
                applicant, 
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _urn = urn;
            _applicant = applicant;
            _number = number;
        }

        public bool Add(Core.Domain.UrnTransaction urnTransaction)
        {
            urnTransaction.AF = _number.GetNewAF(urnTransaction.UrnItemId, System.DateTime.Now.Year);

            SummaryItem(urnTransaction);

            _unitOfWork.UrnTransactions.Add(urnTransaction);
            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.UrnTransaction urnTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveSpaceAF(urnTransaction.AF).ToList();

            if (invoices.Any() && urnTransaction.Price < invoices.Max(i => i.Amount))
                return false;

            SummaryItem(urnTransaction);

            var urnTransactionInDb = GetByAF(urnTransaction.AF);
            urnTransactionInDb.Price = urnTransaction.Price;
            urnTransactionInDb.Remark = urnTransaction.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            if (_unitOfWork.Invoices.GetByActiveUrnAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByUrnAF(AF).Any())
                return false;

            var transactionInDb = _unitOfWork.UrnTransactions.GetByAF(AF);
            _unitOfWork.UrnTransactions.Remove(transactionInDb);
            _unitOfWork.Complete();

            return true;
        }


        private void SummaryItem(Core.Domain.UrnTransaction urnTransaction)
        {
            var item = _unitOfWork.UrnItems.GetActive(urnTransaction.UrnItemId);
            urnTransaction.SummaryItem = "AF: " + urnTransaction.AF + "<BR/>" +
                Resources.Mix.Urn + ": " + item.Urn.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + urnTransaction.Remark;
        }
    }
}