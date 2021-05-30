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
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.NicheId == nicheId && !qt.ToDeleteFlag).OrderByDescending(qt => qt.ActionDate).FirstOrDefault();
        }

        public IEnumerable<ColumbariumTracking> GetTrackingByNicheId(int nicheId, bool toDeleteFlag = false)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.NicheId == nicheId && qt.ToDeleteFlag == toDeleteFlag).OrderByDescending(qt => qt.ActionDate).ToList();
        }

        public ColumbariumTracking GetTrackingByTransactionAF(string columbariumTransactionAF)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.ColumbariumTransactionAF == columbariumTransactionAF && !qt.ToDeleteFlag).SingleOrDefault();
        }

        public ColumbariumTracking GetTrackingByNicheIdAndTransactionAF(int nicheId, string columbariumTransactionAF)
        {
            return MemorialContext.ColumbariumTrackings.Where(qt => qt.ColumbariumTransactionAF == columbariumTransactionAF && qt.NicheId == nicheId && !qt.ToDeleteFlag).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}