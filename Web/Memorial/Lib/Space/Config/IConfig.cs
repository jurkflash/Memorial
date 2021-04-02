using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Space
{
    public interface IConfig
    {
        bool CreateItem(SpaceItemDto spaceItemDto);
        bool CreateSpace(SpaceDto spaceDto);
        bool DeleteItem(int id);
        bool DeleteSpace(int id);
        SpaceItemDto GetItemDto(int id);
        IEnumerable<SpaceItemDto> GetItemDtos();
        SpaceDto GetSpaceDto(int id);
        IEnumerable<SpaceDto> GetSpaceDtos();
        IEnumerable<SpaceNumber> GetNumbers();
        bool UpdateItem(SpaceItemDto spaceItemDto);
        bool UpdateSpace(SpaceDto spaceDto);
    }
}