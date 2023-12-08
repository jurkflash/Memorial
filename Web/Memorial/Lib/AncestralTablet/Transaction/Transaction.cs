using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Lib.AncestralTablet
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IAncestralTablet _ancestralTablet;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected IApplicantDeceased _applicantDeceased;
        protected INumber _number;
        protected Core.Domain.AncestralTabletTransaction _transaction;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item,
            IAncestralTablet ancestralTablet, 
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
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

        public Core.Domain.AncestralTabletTransaction GetByAF(string AF)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByAF(AF);
        }

        public float GetTotalAmount(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction)
        {
            return ancestralTabletTransaction.Price + (ancestralTabletTransaction.Maintenance == null ? 0 : (float)ancestralTabletTransaction.Maintenance);
        }

        public Core.Domain.AncestralTabletTransaction GetExclusive(string AF)
        {
            return _unitOfWork.AncestralTabletTransactions.GetExclusive(AF);
        }

        public IEnumerable<Core.Domain.AncestralTabletTransaction> GetByAncestralTabletIdAndItemId(int ancestralTabletId, int itemId, string filter)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByAncestralTabletIdAndItem(ancestralTabletId, itemId, filter);
        }

        public Core.Domain.AncestralTabletTransaction GetTransactionsByShiftedAncestralTabletTransactionAF(string AF)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByShiftedAncestralTabletTransactionAF(AF);
        }

        public IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestralTabletId(int nicheId)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByAncestralTabletId(nicheId);
        }

        public IEnumerable<Core.Domain.AncestralTabletTransaction> GetRecent(byte? siteId, int? applicantId)
        {
            if (applicantId == null)
                return _unitOfWork.AncestralTabletTransactions.GetRecent(Constant.RecentTransactions, siteId, applicantId);
            else
                return _unitOfWork.AncestralTabletTransactions.GetRecent(null, siteId, applicantId);
        }

        protected bool DeleteAllTransactionWithSameAncestralTabletId()
        {
            var transactions = GetTransactionsByAncestralTabletId(_transaction.AncestralTabletId);

            foreach (var transaction in transactions)
            {
                _unitOfWork.AncestralTabletTransactions.Remove(transaction);
            }

            return true;
        }

        protected bool SetTransactionDeceasedIdBasedOnAncestralTablet(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction, int ancestralTabletId)
        {
            var ancestralTablet = _ancestralTablet.GetById(ancestralTabletId);

            if (ancestralTablet.hasDeceased)
            {
                var deceaseds = _deceased.GetByAncestralTabletId(ancestralTabletId);

                if (deceaseds.Count() == 1)
                {
                    if (_applicantDeceased.GetByApplicantDeceasedId(ancestralTabletTransaction.ApplicantId, deceaseds.ElementAt(0).Id) == null)
                    {
                        return false;
                    }

                    ancestralTabletTransaction.DeceasedId = deceaseds.ElementAt(0).Id;
                }
            }

            return true;
        }

        protected bool ChangeAncestralTablet(string systemCode, int oldAncestralTabletId, int newAncestralTabletId)
        {
            var areaId = _ancestralTablet.GetById(oldAncestralTabletId).AncestralTabletAreaId;

            var itemId = _item.GetByArea(areaId).Where(i => i.SubProductService.SystemCode == systemCode).FirstOrDefault();

            if (itemId == null)
                return false;

            var transactions = GetByAncestralTabletIdAndItemId(oldAncestralTabletId, itemId.Id, null);

            foreach (var transaction in transactions)
            {
                transaction.AncestralTabletId = newAncestralTabletId;
            }

            return true;
        }
    }
}