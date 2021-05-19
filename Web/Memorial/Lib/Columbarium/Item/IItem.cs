using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface IItem
    {
        void SetItem(int id);

        Core.Domain.ColumbariumItem GetItem();

        ColumbariumItemDto GetItemDto();

        Core.Domain.ColumbariumItem GetItem(int id);

        ColumbariumItemDto GetItemDto(int id);

        int GetId();

        string GetName();

        string GetDescription();

        float GetPrice();

        string GetSystemCode();

        bool IsOrder();

        IEnumerable<Core.Domain.ColumbariumItem> GetItemByCentre(int centreId);

        IEnumerable<ColumbariumItemDto> GetItemDtosByCentre(int centreId);

        bool Create(ColumbariumItemDto columbariumItemDto);

        bool Update(Core.Domain.ColumbariumItem columbariumItem);

        bool Delete(int id);
    }
}