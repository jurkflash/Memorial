using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class ColumbariumTrackingRepository : Repository<ColumbariumTracking>, IColumbariumTrackingRepository
    {
        public ColumbariumTrackingRepository(MemorialContext context) : base(context)
        {
        }

        public ColumbariumTracking GetLatestFirstTransactionByQuadrangleId(int quadrangleId)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.QuadrangleId == quadrangleId).OrderByDescending(qt => qt.ActionDate).FirstOrDefault();
        }

        public IEnumerable<ColumbariumTracking> GetTrackingByQuadrangleId(int quadrangleId)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.QuadrangleId == quadrangleId).OrderByDescending(qt => qt.ActionDate).ToList();
        }

        public ColumbariumTracking GetTrackingByTransactionAF(string columbariumTransactionAF)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.ColumbariumTransactionAF == columbariumTransactionAF).SingleOrDefault();
        }

        public ColumbariumTracking GetTrackingByQuadrangleIdAndTransactionAF(int quadrangleId, string columbariumTransactionAF)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.ColumbariumTransactionAF == columbariumTransactionAF && qt.QuadrangleId == quadrangleId).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}