using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotTrackingRepository : IRepository<PlotTracking>
    {
        PlotTracking GetLatestFirstTransactionByPlotId(int plotId);

        IEnumerable<PlotTracking> GetTrackingByPlotId(int plotId);

        PlotTracking GetTrackingByTransactionAF(string plotTransactionAF);

        PlotTracking GetTrackingByPlotIdAndTransactionAF(int plotId, string plotTransactionAF);
    }
}
