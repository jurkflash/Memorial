using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotItemRepository : IRepository<PlotItem>
    {
        IEnumerable<PlotItem> GetByArea(int plotAreaId);
    }
}
