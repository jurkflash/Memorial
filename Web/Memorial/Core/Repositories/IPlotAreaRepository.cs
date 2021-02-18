using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotAreaRepository : IRepository<PlotArea>
    {
        IEnumerable<PlotArea> GetBySite(byte siteId);
    }
}
