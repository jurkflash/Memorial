using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotItemRepository : IRepository<PlotItem>
    {
        PlotItem GetActive(int id);

        IEnumerable<PlotItem> GetAllActive();

        IEnumerable<PlotItem> GetByPlot(int plotId);
    }
}
