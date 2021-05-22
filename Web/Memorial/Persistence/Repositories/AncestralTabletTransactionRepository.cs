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

        public AncestralTabletTransaction GetActive(string AF)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.ShiftedAncestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.AF == AF && at.DeleteDate == null)
                .SingleOrDefault();
        }

        public AncestralTabletTransaction GetExclusive(string AF)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.ShiftedAncestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<AncestralTabletTransaction> GetByApplicant(int id)
        {
            return MemorialContext.AncestralTabletTransactions.Where(at => at.ApplicantId == id
                                            && at.DeleteDate == null).ToList();
        }

        public IEnumerable<AncestralTabletTransaction> GetByAncestorIdAndItem(int ancestorId, int itemId, string filter)
        {
            var transactions = MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.ShiftedAncestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.AncestorItemId == itemId
                                            && (at.AncestorId == ancestorId || at.ShiftedAncestorId == ancestorId)
                                            && at.DeleteDate == null);

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(t => t.AF.Contains(filter) || t.Applicant.Name.Contains(filter) || t.Applicant.Name2.Contains(filter)).ToList();
            }
        }

        public IEnumerable<AncestralTabletTransaction> GetByAncestorIdAndItemAndApplicant(int ancestorId, int itemId, int applicantId)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.ApplicantId == applicantId
                                            && at.AncestorItemId == itemId
                                            && at.AncestorId == ancestorId
                                            && at.DeleteDate == null).ToList();
        }

        public AncestralTabletTransaction GetByShiftedAncestralTabletTransactionAF(string AF)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.ShiftedAncestralTabletTransactionAF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<AncestralTabletTransaction> GetByAncestorId(int ancestorId)
        {
            return MemorialContext.AncestralTabletTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.AncestorId == ancestorId && at.DeleteDate == null)
                .ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}