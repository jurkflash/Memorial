using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;

namespace Memorial.Lib.Miscellaneous
{
    public class Donation : Transaction, IDonation
    {
        private readonly IUnitOfWork _unitOfWork;

        public Donation(
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

            _unitOfWork.MiscellaneousTransactions.Add(miscellaneousTransaction);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.MiscellaneousTransaction miscellaneousTransaction)
        {
            var receipts = _unitOfWork.Receipts.GetByMiscellaneousAF(miscellaneousTransaction.AF).ToList();

            if (receipts.Any() && miscellaneousTransaction.Amount < receipts.Max(i => i.Amount))
                return false;

            var miscellaneousTransactionInDb = GetByAF(miscellaneousTransaction.AF);
            miscellaneousTransactionInDb.Amount = miscellaneousTransaction.Amount;
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
    }
}