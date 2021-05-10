using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleTrackingRepository : IRepository<QuadrangleTracking>
    {
        QuadrangleTracking GetLatestFirstTransactionByQuadrangleId(int quadrangleId);

        IEnumerable<QuadrangleTracking> GetTrackingByQuadrangleId(int quadrangleId);

        QuadrangleTracking GetTrackingByTransactionAF(string quadrangleTransactionAF);

        QuadrangleTracking GetTrackingByQuadrangleIdAndTransactionAF(int quadrangleId, string quadrangleTransactionAF);
    }
}
