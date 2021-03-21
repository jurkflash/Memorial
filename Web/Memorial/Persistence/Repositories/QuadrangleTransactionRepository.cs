using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleTransactionRepository : Repository<QuadrangleTransaction>, IQuadrangleTransactionRepository
    {
        public QuadrangleTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public QuadrangleTransaction GetActive(string AF)
        {
            return MemorialContext.QuadrangleTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Quadrangle)
                .Include(qt => qt.QuadrangleItem)
                .Where(qt => qt.AF == AF && qt.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<QuadrangleTransaction> GetByApplicant(int id)
        {
            return MemorialContext.QuadrangleTransactions.Where(qt => qt.ApplicantId == id
                                            && qt.DeleteDate == null).ToList();
        }

        public IEnumerable<QuadrangleTransaction> GetByQuadrangleIdAndItem(int quadrangleId, int itemId)
        {
            return MemorialContext.QuadrangleTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Quadrangle)
                .Include(qt => qt.ShiftedQuadrangle)
                .Include(qt => qt.QuadrangleItem)
                .Where(qt => qt.QuadrangleItemId == itemId
                                            && (qt.QuadrangleId == quadrangleId || qt.ShiftedQuadrangleId == quadrangleId)
                                            && qt.DeleteDate == null).ToList();
        }

        public IEnumerable<QuadrangleTransaction> GetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId)
        {
            return MemorialContext.QuadrangleTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Quadrangle)
                .Include(qt => qt.QuadrangleItem)
                .Where(qt => qt.ApplicantId == applicantId
                                            && qt.QuadrangleItemId == itemId
                                            && qt.QuadrangleId == quadrangleId
                                            && qt.DeleteDate == null).ToList();
        }

        public QuadrangleTransaction GetLastQuadrangleTransactionByQuadrangleId(int quadrangleId)
        {
            return MemorialContext.QuadrangleTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Quadrangle)
                .Include(qt => qt.QuadrangleItem)
                .Where(qt => qt.QuadrangleId == quadrangleId
                                            && qt.DeleteDate == null).OrderByDescending(qt => qt.CreateDate).FirstOrDefault();
        }

        public QuadrangleTransaction GetLastQuadrangleTransactionByShiftedQuadrangleId(int quadrangleId)
        {
            return MemorialContext.QuadrangleTransactions
                .Include(qt => qt.Applicant)
                .Include(qt => qt.Quadrangle)
                .Include(qt => qt.QuadrangleItem)
                .Where(qt => qt.ShiftedQuadrangleId == quadrangleId
                                            && qt.DeleteDate == null).OrderByDescending(qt => qt.CreateDate).FirstOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}