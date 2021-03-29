using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface IItem
    {
        void SetItem(int id);

        Core.Domain.AncestorItem GetItem();

        AncestorItemDto GetItemDto();

        Core.Domain.AncestorItem GetItem(int id);

        AncestorItemDto GetItemDto(int id);

        int GetId();

        string GetName();

        string GetDescription();

        float GetPrice();

        string GetSystemCode();

        bool IsOrder();

        IEnumerable<Core.Domain.AncestorItem> GetItemByArea(int areaId);

        IEnumerable<AncestorItemDto> GetItemDtosByArea(int areaId);
    }
}