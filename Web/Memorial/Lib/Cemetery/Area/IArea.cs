using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public interface IArea
    {
        void SetArea(int id);

        int GetId();

        string GetName();

        string GetDescription();

        byte GetSiteId();

        Core.Domain.PlotArea GetArea();

        PlotAreaDto GetAreaDto();

        Core.Domain.PlotArea GetArea(int areaId);

        PlotAreaDto GetAreaDto(int areaId);

        IEnumerable<PlotAreaDto> GetAreaDtos();

        IEnumerable<Core.Domain.PlotArea> GetAreaBySite(byte siteId);

        IEnumerable<PlotAreaDto> GetAreaDtosBySite(byte siteId);

        bool Create(PlotAreaDto plotAreaDto);

        bool Update(Core.Domain.PlotArea plotArea);

        bool Delete(int id);

    }
}