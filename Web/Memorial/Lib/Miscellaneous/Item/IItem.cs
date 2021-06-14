using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Miscellaneous
{
    public interface IItem
    {
        void SetItem(int id);

        Core.Domain.MiscellaneousItem GetItem();

        MiscellaneousItemDto GetItemDto();

        Core.Domain.MiscellaneousItem GetItem(int id);

        MiscellaneousItemDto GetItemDto(int id);

        IEnumerable<MiscellaneousItemDto> GetItemDtos();

        int GetId();

        int GetMiscellaneousId();

        string GetName();

        string GetDescription();

        float GetPrice();

        string GetSystemCode();

        bool IsOrder();

        IEnumerable<Core.Domain.MiscellaneousItem> GetItemByMiscellaneous(int miscellaneousId);

        IEnumerable<MiscellaneousItemDto> GetItemDtosByMiscellaneous(int miscellaneousId);

        IEnumerable<SubProductServiceDto> GetAvailableItemDtosByMiscellaneous(int miscellaneousId);

        int Create(MiscellaneousItemDto miscellaneousItemDto);

        bool Update(MiscellaneousItemDto miscellaneousItemDto);

        bool Delete(int id);
    }
}