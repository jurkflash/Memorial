using System.Collections.Generic;

namespace Memorial.Lib.Cremation
{
    public interface IItem
    {
        Core.Domain.CremationItem GetById(int id);
        float GetPrice(Core.Domain.CremationItem cremationItem);
        IEnumerable<Core.Domain.CremationItem> GetByCremation(int cremationId);
        IEnumerable<Core.Domain.SubProductService> GetAvailableItemByCremation(int cremationId);
        int Add(Core.Domain.CremationItem cremationItem);
        bool Change(int id, Core.Domain.CremationItem cremationItem);
        bool Remove(int id);
    }
}