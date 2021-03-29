using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface IArea
    {
        void SetArea(int id);

        int GetId();

        string GetName();

        string GetDescription();

        byte GetSiteId();

        Core.Domain.AncestorArea GetArea();

        AncestorAreaDto GetAreaDto();


        Core.Domain.AncestorArea GetArea(int areaId);

        AncestorAreaDto GetAreaDto(int areaId);

        IEnumerable<Core.Domain.AncestorArea> GetAreaBySite(byte siteId);

        IEnumerable<AncestorAreaDto> GetAreaDtosBySite(byte siteId);

    }
}