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

        public IEnumerable<ColumbariumTransaction> GetByQuadrangleIdAndItem(int quadrangleId, int itemId, string filter)
        {
            var transactions = MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.ShiftedNiche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.ColumbariumItemId == itemId
                                            && (qt.NicheId == quadrangleId || qt.ShiftedNicheId == quadrangleId)
                                            && qt.DeleteDate == null);

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(t=>t.AF.Contains(filter) || t.Applicant.Name.Contains(filter) || t.Applicant.Name2.Contains(filter)).ToList();
            }
        }

        public IEnumerable<ColumbariumTransaction> GetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId)
        {
            return MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.ApplicantId == applicantId
                                            && qt.ColumbariumItemId == itemId
                                            && qt.NicheId == quadrangleId
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

        public IEnumerable<ColumbariumTransaction> GetByQuadrangleId(int quadrangleId)
        {
            return MemorialContext.ColumbariumTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Niche)
                .Include(qt => qt.ColumbariumItem)
                .Where(qt => qt.NicheId == quadrangleId && qt.DeleteDate == null)
                .ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}