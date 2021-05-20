using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotRepository : IRepository<Plot>
    {
        Plot GetActive(int id);

        IEnumerable<Plot> GetByArea(int cemeteryAreaId, string filter);

        IEnumerable<PlotType> GetTypesByArea(int cemeteryAreaId);

        IEnumerable<Plot> GetByTypeAndArea(int cemeteryAreaId, int plotTypeId, string filter);

        IEnumerable<Plot> GetAvailableByTypeAndArea(int plotTypeId, int cemeteryAreaId);

        IEnumerable<Plot> GetBuriedByWithTypeAndArea(int plotTypeID, int cemeteryAreaId);

        IEnumerable<Plot> GetSecondBurialByWithTypeAndArea(int plotTypeID, int cemeteryAreaId);
    }
}
