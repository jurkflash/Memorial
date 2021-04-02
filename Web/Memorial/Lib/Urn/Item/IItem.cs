using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Urn
{
    public interface IItem
    {
        void SetItem(int id);

        Core.Domain.UrnItem GetItem();

        UrnItemDto GetItemDto();

        Core.Domain.UrnItem GetItem(int id);

        UrnItemDto GetItemDto(int id);

        IEnumerable<UrnItemDto> GetItemDtos();

        int GetId();

        int GetUrnId();

        string GetName();

        string GetDescription();

        float GetPrice();

        string GetSystemCode();

        bool IsOrder();

        IEnumerable<Core.Domain.UrnItem> GetItemByUrn(int urnId);

        IEnumerable<UrnItemDto> GetItemDtosByUrn(int urnId);

        bool Create(UrnItemDto urnItemDto);

        bool Update(Core.Domain.UrnItem urnItem);

        bool Delete(int id);
    }
}