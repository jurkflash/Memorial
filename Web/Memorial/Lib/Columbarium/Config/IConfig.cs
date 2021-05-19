using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface IConfig
    {
        bool CreateArea(QuadrangleAreaDto quadrangleAreaDto);
        bool CreateCentre(ColumbariumCentreDto quadrangleCentreDto);
        bool CreateItem(ColumbariumItemDto quadrangleItemDto);
        bool CreateQuadrangle(QuadrangleDto quadrangleDto);
        bool DeleteArea(int id);
        bool DeleteCentre(int id);
        bool DeleteItem(int id);
        bool DeleteQuadrangle(int id);
        IEnumerable<QuadrangleAreaDto> GetAreaDtosByCentre(int centreId);
        ColumbariumItemDto GetItemDto(int id);
        IEnumerable<ColumbariumItemDto> GetItemDtosByCentre(int centreId);
        IEnumerable<ColumbariumNumber> GetNumbers();
        QuadrangleAreaDto GetQuadrangleAreaDto(int id);
        ColumbariumCentreDto GetQuadrangleCentreDto(int id);
        IEnumerable<ColumbariumCentreDto> GetQuadrangleCentreDtos();
        QuadrangleDto GetQuadrangleDto(int id);
        IEnumerable<QuadrangleDto> GetQuadrangleDtosByAreaId(int areaId);
        bool UpdateArea(QuadrangleAreaDto quadrangleAreaDto);
        bool UpdateCentre(ColumbariumCentreDto quadrangleCentreDto);
        bool UpdateItem(ColumbariumItemDto quadrangleItemDto);
        bool UpdateQuadrangle(QuadrangleDto quadrangleDto);
    }
}