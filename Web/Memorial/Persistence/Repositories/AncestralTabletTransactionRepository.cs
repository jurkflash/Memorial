using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestralTabletTransactionRepository : Repository<AncestralTabletTransaction>, IAncestralTabletTransactionRepository
    {
        public AncestralTabletTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public AncestralTabletTransaction GetByAF(string AF)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.AncestralTablet)
                .Include(at => at.ShiftedAncestralTablet)
                .Include(at => at.AncestralTabletItem)
                .Include(at => at.AncestralTabletItem.AncestralTabletArea)
                .Include(at => at.AncestralTabletItem.AncestralTabletArea.Site)
                .Where(at => at.AF == AF)
                .SingleOrDefault();
        }

        public AncestralTabletTransaction GetExclusive(string AF)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.AncestralTablet)
                .Include(at => at.ShiftedAncestralTablet)
                .Include(at => at.AncestralTabletItem)
                .Include(at => at.AncestralTabletItem.AncestralTabletArea)
                .Include(at => at.AncestralTabletItem.AncestralTabletArea.Site)
                .Where(at => at.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<AncestralTabletTransaction> GetByApplicant(int id)
        {
            return MemorialContext.AncestralTabletTransactions.Where(at => at.ApplicantId == id).ToList();
        }

        public bool GetExistsByApplicant(int id)
        {
            return MemorialContext.AncestralTabletTransactions.Where(at => at.ApplicantId == id).Any();
        }

        public bool GetExistsByDeceased(int id)
        {
            return MemorialContext.AncestralTabletTransactions.Where(at => at.DeceasedId == id).Any();
        }

        public IEnumerable<AncestralTabletTransaction> GetByAncestralTabletIdAndItem(int ancestralTabletId, int itemId, string filter)
        {
            var transactions = MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.AncestralTablet)
                .Include(at => at.ShiftedAncestralTablet)
                .Include(at => at.AncestralTabletItem)
                .Where(at => at.AncestralTabletItemId == itemId
                                            && (at.AncestralTabletId == ancestralTabletId || at.ShiftedAncestralTabletId == ancestralTabletId));

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(t => t.AF.Contains(filter) || t.Applicant.Name.Contains(filter) || (t.Applicant.Name2 != null && t.Applicant.Name2.Contains(filter))).ToList();
            }
        }

        public IEnumerable<AncestralTabletTransaction> GetByAncestralTabletIdAndItemAndApplicant(int ancestralTabletId, int itemId, int applicantId)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.AncestralTablet)
                .Include(at => at.AncestralTabletItem)
                .Where(at => at.ApplicantId == applicantId
                                            && at.AncestralTabletItemId == itemId
                                            && at.AncestralTabletId == ancestralTabletId).ToList();
        }

        public AncestralTabletTransaction GetByShiftedAncestralTabletTransactionAF(string AF)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.AncestralTablet)
                .Include(at => at.AncestralTabletItem)
                .Where(at => at.ShiftedAncestralTabletTransactionAF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<AncestralTabletTransaction> GetByAncestralTabletId(int ancestralTabletId)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.AncestralTablet)
                .Include(at => at.AncestralTabletItem)
                .Where(at => at.AncestralTabletId == ancestralTabletId)
                .ToList();
        }

        public IEnumerable<AncestralTabletTransaction> GetRecent(int? number, byte? siteId, int? applicantId)
        {
            var result = MemorialContext.AncestralTabletTransactions
                .Include(t => t.Applicant)
                .Include(t => t.AncestralTablet)
                .Include(t => t.AncestralTabletItem.AncestralTabletArea)
                .Include(t => t.AncestralTabletItem.SubProductService)
                .Include(t => t.AncestralTabletItem.SubProductService.Product);

            if (siteId != null)
                result = result.Where(t => t.AncestralTabletItem.AncestralTabletArea.SiteId == siteId);

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