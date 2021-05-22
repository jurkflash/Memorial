using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestorTrackingRepository : Repository<AncestorTracking>, IAncestorTrackingRepository
    {
        public AncestorTrackingRepository(MemorialContext context) : base(context)
        {
        }

        public AncestorTracking GetLatestFirstTransactionByAncestorId(int ancestorId)
        {
            return MemorialContext.AncestorTrackings.Where(qt => qt.AncestorId == ancestorId).OrderByDescending(qt => qt.ActionDate).FirstOrDefault();
        }

        public IEnumerable<AncestorTracking> GetTrackingByAncestorId(int ancestorId)
        {
            return MemorialContext.AncestorTrackings.Where(qt => qt.AncestorId == ancestorId).OrderByDescending(qt => qt.ActionDate).ToList();
        }

        public AncestorTracking GetTrackingByTransactionAF(string ancestralTabletTransactionAF)
        {
            return MemorialContext.AncestorTrackings.Where(qt => qt.AncestralTabletTransactionAF == ancestralTabletTransactionAF).SingleOrDefault();
        }

        public AncestorTracking GetTrackingByAncestorIdAndTransactionAF(int ancestorId, string ancestralTabletTransactionAF)
        {
            return MemorialContext.AncestorTrackings.Where(qt => qt.AncestralTabletTransactionAF == ancestralTabletTransactionAF && qt.AncestorId == ancestorId).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}