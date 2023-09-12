using System.Collections.Generic;

namespace Memorial.Lib.AncestralTablet
{
    public interface IItem
    {
        Core.Domain.AncestralTabletItem GetById(int id);
        float GetPrice(Core.Domain.AncestralTabletItem ancestralTabletItem);
        IEnumerable<Core.Domain.AncestralTabletItem> GetByArea(int areaId);
        IEnumerable<Core.Domain.SubProductService> GetAvailableItemByArea(int areaId);
        int Add(Core.Domain.AncestralTabletItem ancestralTabletItem);
        bool Change(int id, Core.Domain.AncestralTabletItem ancestralTabletItem);
        bool Remove(int id);
        bool IsOrder(Core.Domain.AncestralTabletItem ancestralTabletItem);
    }
}