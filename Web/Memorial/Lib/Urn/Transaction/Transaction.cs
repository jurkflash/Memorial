using System.Collections.Generic;
using Memorial.Core;
using Memorial.Lib.Applicant;

namespace Memorial.Lib.Urn
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IUrn _urn;
        protected IItem _item;
        protected IApplicant _applicant;
        protected INumber _number;

        public Transaction(
            IUnitOfWork unitOfWork,
            IItem item,
            IUrn urn,
            IApplicant applicant,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _urn = urn;
            _applicant = applicant;
            _number = number;
        }

        public Core.Domain.UrnTransaction GetByAF(string AF)
        {
            return _unitOfWork.UrnTransactions.GetByAF(AF);
        }

        public float GetTotalAmount(Core.Domain.UrnTransaction urnTransaction)
        {
            return urnTransaction.Price;
        }

        public IEnumerable<Core.Domain.UrnTransaction> GetByItemId(int itemId, string filter)
        {
            return _unitOfWork.UrnTransactions.GetByItem(itemId, filter);        
        }

        public IEnumerable<Core.Domain.UrnTransaction> GetRecent(int siteId, int? applicantId)
        {
            if (applicantId == null)
                return _unitOfWork.UrnTransactions.GetRecent(Constant.RecentTransactions, siteId, applicantId);
            else
                return _unitOfWork.UrnTransactions.GetRecent(null, siteId, applicantId);
        }
    }
}