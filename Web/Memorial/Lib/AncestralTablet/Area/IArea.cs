using System.Collections.Generic;

namespace Memorial.Lib.AncestralTablet
{
    public interface IArea
    {
        Core.Domain.AncestralTabletArea GetById(int areaId);
        IEnumerable<Core.Domain.AncestralTabletArea> GetAll();
        IEnumerable<Core.Domain.AncestralTabletArea> GetBySite(int siteId);
        int Add(Core.Domain.AncestralTabletArea ancestralTabletArea);
        bool Change(int id, Core.Domain.AncestralTabletArea ancestralTabletArea);
        bool Remove(int id);
    }
}