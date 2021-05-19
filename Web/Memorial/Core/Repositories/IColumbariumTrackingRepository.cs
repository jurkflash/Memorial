using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumTrackingRepository : IRepository<ColumbariumTracking>
    {
        ColumbariumTracking GetLatestFirstTransactionByNicheId(int quadrangleId);

        IEnumerable<ColumbariumTracking> GetTrackingByNicheId(int quadrangleId);

        ColumbariumTracking GetTrackingByTransactionAF(string columbariumTransactionAF);

        ColumbariumTracking GetTrackingByNicheIdAndTransactionAF(int quadrangleId, string columbariumTransactionAF);
    }
}
