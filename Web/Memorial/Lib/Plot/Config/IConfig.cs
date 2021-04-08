using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Plot
{
    public interface IConfig
    {
        bool CreateArea(PlotAreaDto plotAreaDto);
        bool CreateItem(PlotItemDto plotItemDto);
        bool CreatePlot(PlotDto plotDto);
        bool DeleteArea(int id);
        bool DeleteItem(int id);
        bool DeletePlot(int id);
        PlotItemDto GetItemDto(int id);
        IEnumerable<PlotItemDto> GetItemDtosByPlot(int plotId);
        IEnumerable<PlotNumber> GetNumbers();
        PlotAreaDto GetPlotAreaDto(int id);
        IEnumerable<PlotAreaDto> GetPlotAreaDtos();
        PlotDto GetPlotDto(int id);
        IEnumerable<PlotDto> GetPlotDtosByArea(int areaId);
        bool UpdateArea(PlotAreaDto plotAreaDto);
        bool UpdateItem(PlotItemDto plotItemDto);
        bool UpdatePlot(PlotDto plotDto);
    }
}