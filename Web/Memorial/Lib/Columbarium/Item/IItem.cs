using System;
using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface IItem
    {
        Core.Domain.ColumbariumItem GetById(int id);
        float GetPrice(Core.Domain.ColumbariumItem columbariumItem);
        float GetAmountWithDateRange(int itemId, DateTime from, DateTime to);
        IEnumerable<Core.Domain.ColumbariumItem> GetByCentre(int centreId);
        IEnumerable<Core.Domain.SubProductService> GetAvailableItemByCentre(int centreId);
        int Add(Core.Domain.ColumbariumItem columbariumItem);
        bool Change(int id, Core.Domain.ColumbariumItem columbariumItem);
        bool Remove(int id);
        bool IsOrder(Core.Domain.ColumbariumItem columbariumItem);
    }
}