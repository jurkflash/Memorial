using System.Collections.Generic;
using Memorial.Core.Domain;

namespace Memorial.Lib.Space
{
    public interface IItem
    {
        SpaceItem GetById(int id);
        float GetPrice(Core.Domain.SpaceItem spaceItem);
        bool IsOrder(Core.Domain.SpaceItem spaceItem);
        int Add(Core.Domain.SpaceItem spaceItem);
        bool Change(int id, Core.Domain.SpaceItem spaceItem);
        bool Remove(int id);
        IEnumerable<Core.Domain.SpaceItem> GetBySpace(int spaceId);
        IEnumerable<Core.Domain.SubProductService> GetAvailableItemBySpace(int spaceId);
    }
}