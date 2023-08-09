using System.Collections.Generic;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Space
{
    public interface IItem
    {
        SpaceItem GetById(int id);
        float GetPrice(Core.Domain.SpaceItem spaceItem);

        



        void SetItem(int id);

        Core.Domain.SpaceItem GetItem();

        SpaceItemDto GetItemDto();

        Core.Domain.SpaceItem GetItem(int id);

        SpaceItemDto GetItemDto(int id);

        string GetName();

        float GetPrice();

        bool IsOrder();

        bool AllowDeposit();

        IEnumerable<Core.Domain.SpaceItem> GetItemBySpace(int spaceId);

        IEnumerable<SpaceItemDto> GetItemDtosBySpace(int spaceId);

        IEnumerable<SubProductServiceDto> GetAvailableItemDtosBySpace(int spaceId);

        int Create(SpaceItemDto spaceItemDto);

        bool Update(SpaceItemDto spaceItemDto);

        bool Delete(int id);
    }
}