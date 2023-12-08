using System.Collections.Generic;
using Memorial.Core;
using Memorial.Lib.Applicant;

namespace Memorial.Lib.Cremation
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected ICremation _cremation;
        protected IItem _item;
        protected IApplicant _applicant;
        protected INumber _number;

        public Transaction(
            IUnitOfWork unitOfWork,
            IItem item,
            ICremation cremation,
            IApplicant applicant,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _cremation = cremation;
            _applicant = applicant;
            _number = number;
        }

        public Core.Domain.CremationTransaction GetByAF(string AF)
        {
            return _unitOfWork.CremationTransactions.GetByAF(AF);
        }

        public float GetTotalAmount(Core.Domain.CremationTransaction transaction)
        {
            return transaction.Price;
        }

        public IEnumerable<Core.Domain.CremationTransaction> GetByItemId(int itemId, string filter)
        {
            return _unitOfWork.CremationTransactions.GetByItem(itemId, filter);
        }

        public IEnumerable<Core.Domain.CremationTransaction> GetByItemIdAndDeceasedId(int itemId, int deceasedId)
        {
            return _unitOfWork.CremationTransactions.GetByItemAndDeceased(itemId, deceasedId);
        }

        public IEnumerable<Core.Domain.CremationTransaction> GetRecent(byte? siteId, int? applicantId)
        {
            if (applicantId == null)
                return _unitOfWork.CremationTransactions.GetRecent(Constant.RecentTransactions, siteId, applicantId);
            else
                return _unitOfWork.CremationTransactions.GetRecent(null, siteId, applicantId);
        }
    }
}