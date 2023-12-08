using System.Collections.Generic;
using Memorial.Core;
using Memorial.Lib.Applicant;

namespace Memorial.Lib.Miscellaneous
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IMiscellaneous _miscellaneous;
        protected IItem _item;
        protected IApplicant _applicant;
        protected INumber _number;

        public Transaction(
            IUnitOfWork unitOfWork,
            IItem item,
            IMiscellaneous miscellaneous,
            IApplicant applicant,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _miscellaneous = miscellaneous;
            _applicant = applicant;
            _number = number;
        }

        public Core.Domain.MiscellaneousTransaction GetByAF(string AF)
        {
            return _unitOfWork.MiscellaneousTransactions.GetByAF(AF);
        }

        public float GetTotalAmount(Core.Domain.MiscellaneousTransaction miscellaneousTransaction)
        {
            return miscellaneousTransaction.Amount;
        }

        public IEnumerable<Core.Domain.MiscellaneousTransaction> GetByItemId(int itemId, string filter)
        {
            return _unitOfWork.MiscellaneousTransactions.GetByItem(itemId, filter);
        }

        public IEnumerable<Core.Domain.MiscellaneousTransaction> GetRecent(byte? siteId, int? applicantId)
        {
            if (applicantId == null)
                return _unitOfWork.MiscellaneousTransactions.GetRecent(Constant.RecentTransactions, siteId, applicantId);
            else
                return _unitOfWork.MiscellaneousTransactions.GetRecent(null, siteId, applicantId);
        }
    }
}