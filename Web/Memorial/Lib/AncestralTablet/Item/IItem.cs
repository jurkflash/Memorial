using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
{
    public interface IItem
    {
        void SetItem(int id);

        Core.Domain.AncestralTabletItem GetItem();

        AncestralTabletItemDto GetItemDto();

        Core.Domain.AncestralTabletItem GetItem(int id);

        AncestralTabletItemDto GetItemDto(int id);

        IEnumerable<Core.Domain.AncestralTabletItem> GetItems();

        IEnumerable<AncestralTabletItemDto> GetItemDtos();

        int GetId();

        string GetName();

        string GetDescription();

        float GetPrice();

        string GetSystemCode();

        bool IsOrder();

        IEnumerable<Core.Domain.AncestralTabletItem> GetItemByArea(int areaId);

        IEnumerable<AncestralTabletItemDto> GetItemDtosByArea(int areaId);

        IEnumerable<SubProductServiceDto> GetAvailableItemDtosByArea(int areaId);

        int Create(AncestralTabletItemDto ancestralTabletItemDto);

        bool Update(AncestralTabletItemDto ancestralTabletItemDto);

        bool Delete(int id);

    }
}