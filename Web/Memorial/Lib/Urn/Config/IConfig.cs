using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Urn
{
    public interface IConfig
    {
        bool CreateItem(UrnItemDto urnItemDto);
        bool CreateUrn(UrnDto urnDto);
        bool DeleteItem(int id);
        bool DeleteUrn(int id);
        UrnItemDto GetItemDto(int id);
        IEnumerable<UrnItemDto> GetItemDtos();
        UrnDto GetUrnDto(int id);
        IEnumerable<UrnDto> GetUrnDtos();
        IEnumerable<UrnNumber> GetNumbers();
        bool UpdateItem(UrnItemDto urnItemDto);
        bool UpdateUrn(UrnDto urnDto);
    }
}