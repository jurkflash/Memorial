using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumTrackingRepository : IRepository<ColumbariumTracking>
    {
        ColumbariumTracking GetLatestFirstTransactionByNicheId(int nicheId);

        IEnumerable<ColumbariumTracking> GetTrackingByNicheId(int nicheId);

        ColumbariumTracking GetTrackingByTransactionAF(string columbariumTransactionAF);

        ColumbariumTracking GetTrackingByNicheIdAndTransactionAF(int nicheId, string columbariumTransactionAF);
    }
}
