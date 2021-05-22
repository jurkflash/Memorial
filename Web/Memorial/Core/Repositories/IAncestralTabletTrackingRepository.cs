using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestralTabletTrackingRepository : IRepository<AncestralTabletTracking>
    {
        AncestralTabletTracking GetLatestFirstTransactionByAncestorId(int ancestorId);

        IEnumerable<AncestralTabletTracking> GetTrackingByAncestorId(int ancestorId);

        AncestralTabletTracking GetTrackingByTransactionAF(string ancestralTabletTransactionAF);

        AncestralTabletTracking GetTrackingByAncestorIdAndTransactionAF(int ancestorId, string ancestralTabletTransactionAF);
    }
}
