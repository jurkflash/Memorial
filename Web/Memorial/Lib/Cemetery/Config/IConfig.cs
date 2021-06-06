using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IConfig
    {
        bool CreateArea(CemeteryAreaDto cemeteryAreaDto);
        bool CreateItem(CemeteryItemDto cemeteryItemDto);
        bool CreatePlot(PlotDto plotDto);
        bool DeleteArea(int id);
        bool DeleteItem(int id);
        bool DeletePlot(int id);
        CemeteryItemDto GetItemDto(int id);
        IEnumerable<CemeteryItemDto> GetItemDtosByPlot(int plotId);
        IEnumerable<CemeteryNumber> GetNumbers();
        CemeteryAreaDto GetCemeteryAreaDto(int id);
        IEnumerable<CemeteryAreaDto> GetCemeteryAreaDtos();
        PlotDto GetPlotDto(int id);
        IEnumerable<PlotDto> GetPlotDtosByArea(int areaId, string filter);
        bool UpdateArea(CemeteryAreaDto cemeteryAreaDto);
        bool UpdateItem(CemeteryItemDto cemeteryItemDto);
        bool UpdatePlot(PlotDto plotDto);
    }
}