using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICemeteryTrackingRepository : IRepository<CemeteryTracking>
    {
        CemeteryTracking GetLatestFirstTransactionByPlotId(int plotId);

        IEnumerable<CemeteryTracking> GetTrackingByPlotId(int plotId);

        CemeteryTracking GetTrackingByTransactionAF(string cemeteryTransactionAF);

        CemeteryTracking GetTrackingByPlotIdAndTransactionAF(int plotId, string cemeteryTransactionAF);
    }
}
