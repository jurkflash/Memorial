using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
{
    public interface IArea
    {
        void SetArea(int id);

        int GetId();

        string GetName();

        string GetDescription();

        int GetSiteId();

        Core.Domain.AncestralTabletArea GetArea();

        AncestralTabletAreaDto GetAreaDto();

        Core.Domain.AncestralTabletArea GetArea(int areaId);

        AncestralTabletAreaDto GetAreaDto(int areaId);

        IEnumerable<Core.Domain.AncestralTabletArea> GetAreas();

        IEnumerable<AncestralTabletAreaDto> GetAreaDtos();

        IEnumerable<Core.Domain.AncestralTabletArea> GetAreaBySite(int siteId);

        IEnumerable<AncestralTabletAreaDto> GetAreaDtosBySite(int siteId);

        int Create(AncestralTabletAreaDto ancestralTabletAreaDto);

        bool Update(AncestralTabletAreaDto ancestralTabletAreaDto);

        bool Delete(int id);

    }
}