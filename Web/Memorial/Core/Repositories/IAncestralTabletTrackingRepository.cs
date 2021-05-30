using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestralTabletTrackingRepository : IRepository<AncestralTabletTracking>
    {
        AncestralTabletTracking GetLatestFirstTransactionByAncestralTabletId(int ancestralTabletId);

        IEnumerable<AncestralTabletTracking> GetTrackingByAncestralTabletId(int ancestralTabletId, bool ToDeleteFlag);

        AncestralTabletTracking GetTrackingByTransactionAF(string ancestralTabletTransactionAF);

        AncestralTabletTracking GetTrackingByAncestralTabletIdAndTransactionAF(int ancestralTabletId, string ancestralTabletTransactionAF);
    }
}
