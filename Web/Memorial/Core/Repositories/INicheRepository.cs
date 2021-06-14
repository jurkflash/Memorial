using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface INicheRepository : IRepository<Niche>
    {
        Niche GetActive(int id);

        IEnumerable<Niche> GetByArea(int columbariumAreaId);

        Niche GetByAreaAndPositions(int areaId, int positionX, int positionY);

        IEnumerable<Niche> GetByTypeAndArea(int areaId, int nicheTypeId, string filter = null);

        IEnumerable<Niche> GetAvailableByArea(int columbariumAreaId);

        IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int columbariumAreaId);
    }
}
