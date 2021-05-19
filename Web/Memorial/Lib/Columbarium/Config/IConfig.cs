using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface IConfig
    {
        bool CreateArea(ColumbariumAreaDto quadrangleAreaDto);
        bool CreateCentre(ColumbariumCentreDto quadrangleCentreDto);
        bool CreateItem(ColumbariumItemDto quadrangleItemDto);
        bool CreateQuadrangle(NicheDto quadrangleDto);
        bool DeleteArea(int id);
        bool DeleteCentre(int id);
        bool DeleteItem(int id);
        bool DeleteQuadrangle(int id);
        IEnumerable<ColumbariumAreaDto> GetAreaDtosByCentre(int centreId);
        ColumbariumItemDto GetItemDto(int id);
        IEnumerable<ColumbariumItemDto> GetItemDtosByCentre(int centreId);
        IEnumerable<ColumbariumNumber> GetNumbers();
        ColumbariumAreaDto GetQuadrangleAreaDto(int id);
        ColumbariumCentreDto GetQuadrangleCentreDto(int id);
        IEnumerable<ColumbariumCentreDto> GetQuadrangleCentreDtos();
        NicheDto GetQuadrangleDto(int id);
        IEnumerable<NicheDto> GetQuadrangleDtosByAreaId(int areaId);
        bool UpdateArea(ColumbariumAreaDto quadrangleAreaDto);
        bool UpdateCentre(ColumbariumCentreDto quadrangleCentreDto);
        bool UpdateItem(ColumbariumItemDto quadrangleItemDto);
        bool UpdateQuadrangle(NicheDto quadrangleDto);
    }
}