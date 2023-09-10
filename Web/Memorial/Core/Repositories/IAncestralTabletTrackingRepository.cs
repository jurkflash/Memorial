using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestralTabletTrackingRepository : IRepository<AncestralTabletTracking>
    {
        AncestralTabletTracking GetLatestFirstTransactionByAncestralTabletId(int ancestralTabletId);

        IEnumerable<AncestralTabletTracking> GetByAncestralTabletId(int ancestralTabletId, bool ToDeleteFlag);

        AncestralTabletTracking GetByAF(string ancestralTabletTransactionAF);

        AncestralTabletTracking GetByAncestralTabletIdAndTransactionAF(int ancestralTabletId, string ancestralTabletTransactionAF);
    }
}
