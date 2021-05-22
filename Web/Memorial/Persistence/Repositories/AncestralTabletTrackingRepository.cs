using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestralTabletTrackingRepository : Repository<AncestralTabletTracking>, IAncestralTabletTrackingRepository
    {
        public AncestralTabletTrackingRepository(MemorialContext context) : base(context)
        {
        }

        public AncestralTabletTracking GetLatestFirstTransactionByAncestorId(int ancestorId)
        {
            return MemorialContext.AncestralTabletTrackings.Where(qt => qt.AncestorId == ancestorId).OrderByDescending(qt => qt.ActionDate).FirstOrDefault();
        }

        public IEnumerable<AncestralTabletTracking> GetTrackingByAncestorId(int ancestorId)
        {
            return MemorialContext.AncestralTabletTrackings.Where(qt => qt.AncestorId == ancestorId).OrderByDescending(qt => qt.ActionDate).ToList();
        }

        public AncestralTabletTracking GetTrackingByTransactionAF(string ancestralTabletTransactionAF)
        {
            return MemorialContext.AncestralTabletTrackings.Where(qt => qt.AncestralTabletTransactionAF == ancestralTabletTransactionAF).SingleOrDefault();
        }

        public AncestralTabletTracking GetTrackingByAncestorIdAndTransactionAF(int ancestorId, string ancestralTabletTransactionAF)
        {
            return MemorialContext.AncestralTabletTrackings.Where(qt => qt.AncestralTabletTransactionAF == ancestralTabletTransactionAF && qt.AncestorId == ancestorId).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}