using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Ancestor
{
    public interface IConfig
    {
        bool CreateAncestor(AncestorDto ancestorDto);
        bool CreateArea(AncestralTabletAreaDto ancestralTabletAreaDto);
        bool CreateItem(AncestorItemDto ancestorItemDto);
        bool DeleteAncestor(int id);
        bool DeleteArea(int id);
        bool DeleteItem(int id);
        AncestorDto GetAncestorDto(int id);
        IEnumerable<AncestorDto> GetAncestorsByAreaId(int id);
        AncestralTabletAreaDto GetAreaDto(int id);
        IEnumerable<AncestralTabletAreaDto> GetAreaDtos();
        AncestorItemDto GetItemDto(int id);
        IEnumerable<AncestorItemDto> GetItemDtos();
        IEnumerable<AncestorNumber> GetNumbers();
        bool UpdateAncestor(AncestorDto ancestorDto);
        bool UpdateArea(AncestralTabletAreaDto ancestralTabletAreaDto);
        bool UpdateItem(AncestorItemDto ancestorItemDto);
    }
}