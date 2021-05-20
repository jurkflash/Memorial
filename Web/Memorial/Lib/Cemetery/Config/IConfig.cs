using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IConfig
    {
        bool CreateArea(CemeteryAreaDto cemeteryAreaDto);
        bool CreateItem(PlotItemDto plotItemDto);
        bool CreatePlot(PlotDto plotDto);
        bool DeleteArea(int id);
        bool DeleteItem(int id);
        bool DeletePlot(int id);
        PlotItemDto GetItemDto(int id);
        IEnumerable<PlotItemDto> GetItemDtosByPlot(int plotId);
        IEnumerable<PlotNumber> GetNumbers();
        CemeteryAreaDto GetCemeteryAreaDto(int id);
        IEnumerable<CemeteryAreaDto> GetCemeteryAreaDtos();
        PlotDto GetPlotDto(int id);
        IEnumerable<PlotDto> GetPlotDtosByArea(int areaId, string filter);
        bool UpdateArea(CemeteryAreaDto cemeteryAreaDto);
        bool UpdateItem(PlotItemDto plotItemDto);
        bool UpdatePlot(PlotDto plotDto);
    }
}