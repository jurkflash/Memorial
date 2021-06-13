using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IPlotType
    {
        int GetId();
        string GetName();
        byte GetNumberOfPlacement();
        Core.Domain.PlotType GetPlotType();
        Core.Domain.PlotType GetPlotType(int plotTypeId);
        PlotTypeDto GetPlotTypeDto();
        PlotTypeDto GetPlotTypeDto(int plotTypeId);
        IEnumerable<PlotTypeDto> GetPlotTypeDtos();
        IEnumerable<Core.Domain.PlotType> GetPlotTypes();
        bool isFengShuiPlot();
        void SetPlotType(int id);
    }
}