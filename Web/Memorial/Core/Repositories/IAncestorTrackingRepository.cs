using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorTrackingRepository : IRepository<AncestorTracking>
    {
        AncestorTracking GetLatestFirstTransactionByAncestorId(int ancestorId);

        IEnumerable<AncestorTracking> GetTrackingByAncestorId(int ancestorId);

        AncestorTracking GetTrackingByTransactionAF(string ancestralTabletTransactionAF);

        AncestorTracking GetTrackingByAncestorIdAndTransactionAF(int ancestorId, string ancestralTabletTransactionAF);
    }
}
