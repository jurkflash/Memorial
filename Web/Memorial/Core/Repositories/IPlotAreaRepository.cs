using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotAreaRepository : IRepository<PlotArea>
    {
        PlotArea GetActive(int id);

        IEnumerable<PlotArea> GetAllActive();

        IEnumerable<PlotArea> GetBySite(byte siteId);
    }
}
