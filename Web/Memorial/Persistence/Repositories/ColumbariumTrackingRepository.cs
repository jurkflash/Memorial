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

        public ColumbariumTracking GetLatestFirstTransactionByNicheId(int nicheId)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.NicheId == nicheId).OrderByDescending(qt => qt.ActionDate).FirstOrDefault();
        }

        public IEnumerable<ColumbariumTracking> GetTrackingByNicheId(int nicheId)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.NicheId == nicheId).OrderByDescending(qt => qt.ActionDate).ToList();
        }

        public ColumbariumTracking GetTrackingByTransactionAF(string columbariumTransactionAF)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.ColumbariumTransactionAF == columbariumTransactionAF).SingleOrDefault();
        }

        public ColumbariumTracking GetTrackingByNicheIdAndTransactionAF(int nicheId, string columbariumTransactionAF)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.ColumbariumTransactionAF == columbariumTransactionAF && qt.NicheId == nicheId).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}