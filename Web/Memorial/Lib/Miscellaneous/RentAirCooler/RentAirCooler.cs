using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Core.Domain;

namespace Memorial.Lib.Miscellaneous
{
    public class RentAirCooler : Transaction, IRentAirCooler
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentAirCooler(
            IUnitOfWork unitOfWork,
            IItem item,
            IMiscellaneous miscellaneous,
            IApplicant applicant,
            INumber number
            ) : 
            base(
                unitOfWork, 
                item,
                miscellaneous, 
                applicant, 
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _miscellaneous = miscellaneous;
            _applicant = applicant;
            _number = number;
        }

        public bool Add(Core.Domain.MiscellaneousTransaction miscellaneousTransaction)
        {
            miscellaneousTransaction.AF = _number.GetNewAF(miscellaneousTransaction.MiscellaneousItemId, System.DateTime.Now.Year);

            SummaryItem(miscellaneousTransaction);

            _unitOfWork.MiscellaneousTransactions.Add(miscellaneousTransaction);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.MiscellaneousTransaction miscellaneousTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveMiscellaneousAF(miscellaneousTransaction.AF).ToList();

            if (invoices.Any() && miscellaneousTransaction.Amount < invoices.Max(i => i.Amount))
                return false;

            SummaryItem(miscellaneousTransaction);

            var miscellaneousTransactionInDb = GetByAF(miscellaneousTransaction.AF);
            miscellaneousTransactionInDb.Amount = miscellaneousTransaction.Amount;
            miscellaneousTransactionInDb.SummaryItem = miscellaneousTransaction.SummaryItem;
            miscellaneousTransactionInDb.Remark = miscellaneousTransaction.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            if (_unitOfWork.Invoices.GetByActiveMiscellaneousAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByMiscellaneousAF(AF).Any())
                return false;

            var transactionInDb = _unitOfWork.MiscellaneousTransactions.GetByAF(AF);
            _unitOfWork.MiscellaneousTransactions.Remove(transactionInDb);
            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(MiscellaneousTransaction miscellaneousTransaction)
        {
            miscellaneousTransaction.SummaryItem = "AF: " + miscellaneousTransaction.AF + "<BR/>" +
                Resources.Mix.Remark + ": " + miscellaneousTransaction.Remark;
        }
    }
}