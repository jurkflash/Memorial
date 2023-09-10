using System.Collections.Generic;

namespace Memorial.Lib.Urn
{
    public interface IItem
    {
        Core.Domain.UrnItem GetById(int id);
        IEnumerable<Core.Domain.UrnItem> GetByUrn(int urnId);
        IEnumerable<Core.Domain.SubProductService> GetAvailableItemByUrn(int urnId);
        int Add(Core.Domain.UrnItem urnItem);
        bool Change(int id, Core.Domain.UrnItem urnItem);
        bool Remove(int id);
    }
}