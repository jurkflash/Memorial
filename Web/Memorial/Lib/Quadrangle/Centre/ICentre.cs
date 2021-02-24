using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface ICentre
    {
        void SetCentre(int id);

        void SetCentre(Core.Domain.QuadrangleCentre quadrangleCentre);

        int GetID();

        string GetName();

        string GetDescription();

        Core.Domain.QuadrangleCentre GetCentre();

        IEnumerable<Core.Domain.QuadrangleCentre> GetBySite(byte sitId);

        IEnumerable<QuadrangleCentreDto> DtosGetBySite(byte siteId);
    }
}