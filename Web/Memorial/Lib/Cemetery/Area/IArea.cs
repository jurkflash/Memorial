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

        Core.Domain.CemeteryArea GetArea();

        CemeteryAreaDto GetAreaDto();

        Core.Domain.CemeteryArea GetArea(int areaId);

        CemeteryAreaDto GetAreaDto(int areaId);

        IEnumerable<CemeteryAreaDto> GetAreaDtos();

        IEnumerable<Core.Domain.CemeteryArea> GetAreaBySite(byte siteId);

        IEnumerable<CemeteryAreaDto> GetAreaDtosBySite(byte siteId);

        bool Create(CemeteryAreaDto cemeteryAreaDto);

        bool Update(Core.Domain.CemeteryArea cemeteryArea);

        bool Delete(int id);

    }
}