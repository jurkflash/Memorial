using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotRepository : IRepository<Plot>
    {
        Plot GetActive(int id);

        IEnumerable<Plot> GetByArea(int plotAreaId);

        IEnumerable<Plot> GetByTypeAndArea(int plotTypeID, int plotAreaId);

        IEnumerable<Plot> GetAvailableByTypeAndArea(int plotTypeId, int plotAreaId);

        IEnumerable<Plot> GetBuriedByWithTypeAndArea(int plotTypeID, int plotAreaId);

        IEnumerable<Plot> GetSecondBurialByWithTypeAndArea(int plotTypeID, int plotAreaId);
    }
}
