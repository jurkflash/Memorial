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
                .Where(qt => qt.AF == AF)
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
            return MemorialContext.ColumbariumTransactions.Where(qt => qt.ApplicantId == id).ToList();
        }

        public bool GetExistsByApplicant(int id)
        {
            return MemorialContext.ColumbariumTransactions.Where(qt => qt.ApplicantId == id).Any();
        }

        public bool GetExistsByDeceased(int id)
        {
            return MemorialContext.ColumbariumTransactions.Where(qt => qt.Deceased1Id == id || qt.Deceased2Id == id).Any();
        }

        public IEnumerable<ColumbariumTransaction> GetByNicheIdAndItem(int nicheId, int itemId, string filter)
        {
            var transactions = MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.ShiftedNiche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.ColumbariumItemId == itemId
                                            && (qt.NicheId == nicheId || qt.ShiftedNicheId == nicheId));

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
                                            && qt.NicheId == nicheId).ToList();
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
                .Where(qt => qt.NicheId == nicheId)
                .ToList();
        }

        public IEnumerable<ColumbariumTransaction> GetRecent(int? number, int siteId, int? applicantId)
        {
            var result = MemorialContext.ColumbariumTransactions
                .Where(t => t.ColumbariumItem.ColumbariumCentre.SiteId == siteId)
                .Include(t => t.Applicant)
                .Include(t => t.ColumbariumItem)
                .Include(t => t.Niche)
                .Include(t => t.Niche.ColumbariumArea)
                .Include(t => t.Niche.ColumbariumArea.ColumbariumCentre)
                .Include(t => t.ColumbariumItem.SubProductService)
                .Include(t => t.ColumbariumItem.SubProductService.Product);

            if (applicantId != null)
                result = result.Where(t => t.ApplicantId == applicantId);

            if (number != null)
                result = result.Take((int)number);

            return result.OrderByDescending(t => t.CreatedUtcTime).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}