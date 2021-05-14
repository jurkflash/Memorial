using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.PlotLandscapeCompany
{
    public interface IPlotLandscapeCompany
    {
        bool CreatePlotLandscapeCompany(PlotLandscapeCompanyDto PlotLandscapeCompanyDto);
        bool DeletePlotLandscapeCompany(int id);
        Core.Domain.PlotLandscapeCompany GetPlotLandscapeCompany();
        Core.Domain.PlotLandscapeCompany GetPlotLandscapeCompany(int id);
        PlotLandscapeCompanyDto GetPlotLandscapeCompanyDto(int id);
        IEnumerable<PlotLandscapeCompanyDto> GetPlotLandscapeCompanyDtos();
        void SetPlotLandscapeCompany(int id);
        bool UpdatePlotLandscapeCompany(PlotLandscapeCompanyDto PlotLandscapeCompanyDto);
    }
}