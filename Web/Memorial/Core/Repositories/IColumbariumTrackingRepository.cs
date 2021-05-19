using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumTrackingRepository : IRepository<ColumbariumTracking>
    {
        ColumbariumTracking GetLatestFirstTransactionByQuadrangleId(int quadrangleId);

        IEnumerable<ColumbariumTracking> GetTrackingByQuadrangleId(int quadrangleId);

        ColumbariumTracking GetTrackingByTransactionAF(string columbariumTransactionAF);

        ColumbariumTracking GetTrackingByQuadrangleIdAndTransactionAF(int quadrangleId, string columbariumTransactionAF);
    }
}
