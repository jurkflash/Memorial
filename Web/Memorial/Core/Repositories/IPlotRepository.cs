using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotRepository : IRepository<Plot>
    {
        IEnumerable<Plot> GetByTypeAndArea(int plotTypeID, int plotAreaId);

        IEnumerable<Plot> GetBuriedByWithTypeAndArea(int plotTypeID, int plotAreaId);

        IEnumerable<Plot> GetSecondBurialByWithTypeAndArea(int plotTypeID, int plotAreaId);
    }
}
