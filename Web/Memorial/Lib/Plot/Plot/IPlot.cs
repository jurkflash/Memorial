using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public interface IPlot
    {
        void SetPlot(int id);

        Core.Domain.Plot GetPlot();

        PlotDto GetPlotDto();

        Core.Domain.Plot GetPlot(int id);

        PlotDto GetPlotDto(int id);

        IEnumerable<Core.Domain.Plot> GetPlotsByAreaId(int id);

        IEnumerable<PlotDto> GetPlotDtosByAreaId(int id);

        IEnumerable<Core.Domain.Plot> GetAvailablePlotsByTypeIdAndAreaId(int typeId, int areaId);

        IEnumerable<PlotDto> GetAvailablePlotDtosByAreaId(int typeId, int areaId);

        string GetName();

        string GetDescription();

        string GetSize();

        float GetPrice();

        float GetMaintenance();

        float GetWall();

        float GetDig();

        float GetBrick();

        string GetRemark();

        bool HasDeceased();

        void SetHasDeceased(bool flag);

        bool HasCleared();

        void SetHasCleared(bool flag);

        bool HasApplicant();

        int? GetApplicantId();

        void SetApplicant(int applicantId);

        void RemoveApplicant();

        int GetAreaId();

        int GetNumberOfPlacement();

        bool IsFengShuiPlot();

        bool Create(PlotDto plotDto);

        bool Update(Core.Domain.Plot plot);

        bool Delete(int id);
    }
}