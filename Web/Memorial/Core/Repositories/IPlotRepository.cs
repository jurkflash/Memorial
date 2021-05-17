using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotRepository : IRepository<Plot>
    {
        Plot GetActive(int id);

        IEnumerable<Plot> GetByArea(int plotAreaId, string filter);

        IEnumerable<PlotType> GetTypesByArea(int plotAreaId);

        IEnumerable<Plot> GetByTypeAndArea(int plotAreaId, int plotTypeId, string filter);

        IEnumerable<Plot> GetAvailableByTypeAndArea(int plotTypeId, int plotAreaId);

        IEnumerable<Plot> GetBuriedByWithTypeAndArea(int plotTypeID, int plotAreaId);

        IEnumerable<Plot> GetSecondBurialByWithTypeAndArea(int plotTypeID, int plotAreaId);
    }
}
