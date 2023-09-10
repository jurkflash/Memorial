using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Lib.Columbarium
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INiche _niche;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected IApplicantDeceased _applicantDeceased;
        protected INumber _number;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item, 
            INiche niche, 
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _niche = niche;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public Core.Domain.ColumbariumTransaction GetByAF(string AF)
        {
            return _unitOfWork.ColumbariumTransactions.GetByAF(AF);
        }

        public float GetTotalAmount(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            return columbariumTransaction.Price +
                (columbariumTransaction.Maintenance == null ? 0 : (float)columbariumTransaction.Maintenance) +
                (columbariumTransaction.LifeTimeMaintenance == null ? 0 : (float)columbariumTransaction.LifeTimeMaintenance);
        }

        public Core.Domain.ColumbariumTransaction GetTransactionExclusive(string AF)
        {
            return _unitOfWork.ColumbariumTransactions.GetExclusive(AF);
        }

        public IEnumerable<Core.Domain.ColumbariumTransaction> GetByNicheIdAndItemId(int nicheId, int itemId, string filter)
        {
            return _unitOfWork.ColumbariumTransactions.GetByNicheIdAndItem(nicheId, itemId, filter);
        }

        public Core.Domain.ColumbariumTransaction GetByShiftedColumbariumTransactionAF(string AF)
        {
            return _unitOfWork.ColumbariumTransactions.GetByShiftedColumbariumTransactionAF(AF);
        }

        public IEnumerable<Core.Domain.ColumbariumTransaction> GetByNicheId (int nicheId)
        {
            return _unitOfWork.ColumbariumTransactions.GetByNicheId(nicheId);
        }

        public IEnumerable<Core.Domain.ColumbariumTransaction> GetRecent(int siteId, int? applicantId)
        {
            if (applicantId == null)
                return _unitOfWork.ColumbariumTransactions.GetRecent(Constant.RecentTransactions, siteId, applicantId);
            else
                return _unitOfWork.ColumbariumTransactions.GetRecent(null, siteId, applicantId);
        }

        protected bool DeleteAllTransactionWithSameNicheId(int nicheId)
        {
            var transactions = GetByNicheId(nicheId);

            foreach(var transaction in transactions)
            {
                _unitOfWork.ColumbariumTransactions.Remove(transaction);
            }

            return true;
        }

        protected bool SetTransactionDeceasedIdBasedOnNiche(Core.Domain.ColumbariumTransaction columbariumTransaction, int nicheId)
        {
            var niche = _niche.GetById(nicheId);

            if (niche.hasDeceased)
            {
                var deceaseds = _deceased.GetByNicheId(nicheId);

                if (niche.NicheType.NumberOfPlacement < deceaseds.Count())
                    return false;

                if (deceaseds.Count() > 1)
                {
                    if (_applicantDeceased.GetByApplicantDeceasedId(columbariumTransaction.ApplicantId, deceaseds.ElementAt(1).Id) == null)
                    {
                        return false;
                    }

                    columbariumTransaction.Deceased2Id = deceaseds.ElementAt(1).Id;
                }

                if (deceaseds.Count() == 1)
                {
                    if (_applicantDeceased.GetByApplicantDeceasedId(columbariumTransaction.ApplicantId, deceaseds.ElementAt(0).Id) == null)
                    {
                        return false;
                    }

                    columbariumTransaction.Deceased1Id = deceaseds.ElementAt(0).Id;
                }
            }

            return true;
        }

        protected bool ChangeNiche(string systemCode, int oldNicheId, int newNicheId)
        {
            var centreId = _niche.GetById(oldNicheId).ColumbariumArea.ColumbariumCentreId;

            var itemId = _item.GetByCentre(centreId).Where(i => i.SubProductService.SystemCode == systemCode).FirstOrDefault();

            if (itemId == null)
                return false;

            var transactions = GetByNicheIdAndItemId(oldNicheId, itemId.Id, null);

            foreach (var transaction in transactions)
            {
                transaction.NicheId = newNicheId;
            }

            return true;
        }
    }
}