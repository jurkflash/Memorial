using System.Collections.Generic;

namespace Memorial.Lib.AncestralTablet
{
    public interface IAncestralTablet
    {
        Core.Domain.AncestralTablet GetById(int id);
        IEnumerable<Core.Domain.AncestralTablet> GetByAreaId(int id);
        IEnumerable<Core.Domain.AncestralTablet> GetAvailableAncestralTabletsByAreaId(int id);
        Core.Domain.AncestralTablet GetByAreaIdAndPostions(int areaId, int positionX, int positionY);
        IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId);
        int Add(Core.Domain.AncestralTablet ancestralTablet);
        bool Change(int id, Core.Domain.AncestralTablet ancestralTablet);
        bool Remove(int id);
    }
}