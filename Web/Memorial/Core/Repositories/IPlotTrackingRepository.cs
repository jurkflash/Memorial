using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotTrackingRepository : IRepository<PlotTracking>
    {
        PlotTracking GetLatestFirstTransactionByPlotId(int quadrangleId);

        IEnumerable<PlotTracking> GetTrackingByPlotId(int quadrangleId);

        IEnumerable<PlotTracking> GetTrackingByTransactionAF(string quadrangleTransactionAF);

        PlotTracking GetTrackingByPlotIdAndTransactionAF(int quadrangleId, string quadrangleTransactionAF);
    }
}
