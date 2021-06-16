using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class ColumbariumTransactionRepository : Repository<ColumbariumTransaction>, IColumbariumTransactionRepository
    {
        public ColumbariumTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public ColumbariumTransaction GetActive(string AF)
        {
            return MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.Niche.ColumbariumArea)
                .Include(qt => qt.Niche.ColumbariumArea.ColumbariumCentre)
                .Include(qt => qt.Niche.ColumbariumArea.ColumbariumCentre.Site)
                .Include(qt => qt.ShiftedNiche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.AF == AF && qt.DeleteDate == null)
                .SingleOrDefault();
        }

        public ColumbariumTransaction GetExclusive(string AF)
        {
            return MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.Niche.ColumbariumArea)
                .Include(qt => qt.Niche.ColumbariumArea.ColumbariumCentre)
                .Include(qt => qt.Niche.ColumbariumArea.ColumbariumCentre.Site)
                .Include(qt => qt.ShiftedNiche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<ColumbariumTransaction> GetByApplicant(int id)
        {
            return MemorialContext.ColumbariumTransactions.Where(qt => qt.ApplicantId == id
                                            && qt.DeleteDate == null).ToList();
        }

        public IEnumerable<ColumbariumTransaction> GetByNicheIdAndItem(int nicheId, int itemId, string filter)
        {
            var transactions = MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.ShiftedNiche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.ColumbariumItemId == itemId
                                            && (qt.NicheId == nicheId || qt.ShiftedNicheId == nicheId)
                                            && qt.DeleteDate == null);

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(t=>t.AF.Contains(filter) || t.Applicant.Name.Contains(filter) || (t.Applicant.Name2 != null && t.Applicant.Name2.Contains(filter))).ToList();
            }
        }

        public IEnumerable<ColumbariumTransaction> GetByNicheIdAndItemAndApplicant(int nicheId, int itemId, int applicantId)
        {
            return MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.ApplicantId == applicantId
                                            && qt.ColumbariumItemId == itemId
                                            && qt.NicheId == nicheId
                                            && qt.DeleteDate == null).ToList();
        }

        public ColumbariumTransaction GetByShiftedColumbariumTransactionAF(string AF)
        {
            return MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.ShiftedColumbariumTransactionAF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<ColumbariumTransaction> GetByNicheId(int nicheId)
        {
            return MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.NicheId == nicheId && qt.DeleteDate == null)
                .ToList();
        }

        public IEnumerable<ColumbariumTransaction> GetRecent(int number, int siteId)
        {
            return MemorialContext.ColumbariumTransactions
                .Where(t => t.DeleteDate == null && t.ColumbariumItem.ColumbariumCentre.SiteId == siteId)
                .Include(t => t.Applicant)
                .Include(t => t.ColumbariumItem)
                .Include(t => t.Niche)
                .Include(t => t.Niche.ColumbariumArea)
                .Include(t => t.Niche.ColumbariumArea.ColumbariumCentre)
                .Include(t => t.ColumbariumItem.SubProductService)
                .Include(t => t.ColumbariumItem.SubProductService.Product)
                .OrderByDescending(t => t.CreateDate)
                .Take(number)
                .ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}