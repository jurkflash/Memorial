using System.Collections.Generic;

namespace Memorial.Lib.Miscellaneous
{
    public interface IItem
    {
        Core.Domain.MiscellaneousItem GetById(int id);
        float GetPrice(Core.Domain.MiscellaneousItem miscellaneousItem);
        IEnumerable<Core.Domain.MiscellaneousItem> GetByMiscellaneous(int miscellaneousId);
        IEnumerable<Core.Domain.SubProductService> GetAvailableItemByMiscellaneous(int miscellaneousId);
        int Add(Core.Domain.MiscellaneousItem miscellaneousItem);
        bool Change(int id, Core.Domain.MiscellaneousItem miscellaneousItem);
        bool Remove(int id);
        bool IsOrder(Core.Domain.MiscellaneousItem miscellaneousItem);
    }
}