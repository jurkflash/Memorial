using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.AncestralTablet
{
    public interface IConfig
    {
        bool CreateAncestralTablet(AncestralTabletDto ancestralTabletDto);
        bool CreateArea(AncestralTabletAreaDto ancestralTabletAreaDto);
        bool CreateItem(AncestralTabletItemDto ancestralTabletItemDto);
        bool DeleteAncestralTablet(int id);
        bool DeleteArea(int id);
        bool DeleteItem(int id);
        AncestralTabletDto GetAncestralTabletDto(int id);
        IEnumerable<AncestralTabletDto> GetAncestralTabletsByAreaId(int id);
        AncestralTabletAreaDto GetAreaDto(int id);
        IEnumerable<AncestralTabletAreaDto> GetAreaDtos();
        AncestralTabletItemDto GetItemDto(int id);
        IEnumerable<AncestralTabletItemDto> GetItemDtos();
        IEnumerable<AncestralTabletNumber> GetNumbers();
        bool UpdateAncestralTablet(AncestralTabletDto ancestralTabletDto);
        bool UpdateArea(AncestralTabletAreaDto ancestralTabletAreaDto);
        bool UpdateItem(AncestralTabletItemDto ancestralTabletItemDto);
    }
}