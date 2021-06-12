using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestralTabletRepository : IRepository<AncestralTablet>
    {
        AncestralTablet GetActive(int id);

        IEnumerable<AncestralTablet> GetByArea(int ancestralTabletAreaId);

        AncestralTablet GetByAreaAndPositions(int areaId, int positionX, int positionY);

        IEnumerable<AncestralTablet> GetAvailableByArea(int ancestralTabletAreaId);

        IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int ancestralTabletAreaId);
    }
}
