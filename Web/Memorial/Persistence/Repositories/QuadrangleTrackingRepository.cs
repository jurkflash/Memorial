using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleTrackingRepository : Repository<QuadrangleTracking>, IQuadrangleTrackingRepository
    {
        public QuadrangleTrackingRepository(MemorialContext context) : base(context)
        {
        }

        public QuadrangleTracking GetLatestFirstTransactionByQuadrangleId(int quadrangleId)
        {
            return MemorialContext.QuadrangleTrackings.Where(qt => qt.QuadrangleId == quadrangleId).OrderByDescending(qt => qt.ActionDate).FirstOrDefault();
        }

        public IEnumerable<QuadrangleTracking> GetTrackingByQuadrangleId(int quadrangleId)
        {
            return MemorialContext.QuadrangleTrackings.Where(qt => qt.QuadrangleId == quadrangleId).OrderByDescending(qt => qt.ActionDate).ToList();
        }

        public QuadrangleTracking GetTrackingByTransactionAF(string quadrangleTransactionAF)
        {
            return MemorialContext.QuadrangleTrackings.Where(qt => qt.QuadrangleTransactionAF == quadrangleTransactionAF).SingleOrDefault();
        }

        public QuadrangleTracking GetTrackingByQuadrangleIdAndTransactionAF(int quadrangleId, string quadrangleTransactionAF)
        {
            return MemorialContext.QuadrangleTrackings.Where(qt => qt.QuadrangleTransactionAF == quadrangleTransactionAF && qt.QuadrangleId == quadrangleId).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}