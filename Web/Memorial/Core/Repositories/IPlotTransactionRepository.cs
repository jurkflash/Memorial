using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotTransactionRepository : IRepository<PlotTransaction>
    {
        PlotTransaction GetActive(string AF);

        PlotTransaction GetExclusive(string AF);

        IEnumerable<PlotTransaction> GetByApplicant(int id);

        IEnumerable<PlotTransaction> GetByPlotIdAndItem(int plotId, int itemId);

        PlotTransaction GetByPlotIdAndDeceased(int plotId, int deceased1Id);

        PlotTransaction GetLastPlotTransactionByPlotId(int plotId);

        PlotTransaction GetLastPlotTransactionByShiftedPlotId(int plotId);

        IEnumerable<PlotTransaction> GetByPlotId(int plotId);

        IEnumerable<PlotTransaction> GetByPlotIdAndItemAndApplicant(int plotId, int itemId, int applicantId);

    }
}
