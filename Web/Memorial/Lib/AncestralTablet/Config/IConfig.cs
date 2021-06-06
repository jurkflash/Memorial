using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.AncestralTablet
{
    public interface IConfig
    {
        int CreateAncestralTablet(AncestralTabletDto ancestralTabletDto);
        int CreateArea(AncestralTabletAreaDto ancestralTabletAreaDto);
        int CreateItem(AncestralTabletItemDto ancestralTabletItemDto);
        bool DeleteAncestralTablet(int id);
        bool DeleteArea(int id);
        bool DeleteItem(int id);
        AncestralTabletDto GetAncestralTabletDto(int id);
        IEnumerable<AncestralTabletDto> GetAncestralTabletDtosByAreaId(int id);
        AncestralTabletAreaDto GetAreaDto(int id);
        IEnumerable<AncestralTabletAreaDto> GetAreaDtos();
        AncestralTabletItemDto GetItemDto(int id);
        IEnumerable<AncestralTabletItemDto> GetItemDtosByAreaId(int areaId);
        IEnumerable<AncestralTabletNumber> GetNumbers();
        bool UpdateAncestralTablet(AncestralTabletDto ancestralTabletDto);
        bool UpdateArea(AncestralTabletAreaDto ancestralTabletAreaDto);
        bool UpdateItem(AncestralTabletItemDto ancestralTabletItemDto);
    }
}