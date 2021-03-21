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

        Core.Domain.QuadrangleCentre GetCentre(int id);

        QuadrangleCentreDto GetCentreDto(int id);

        IEnumerable<Core.Domain.QuadrangleCentre> GetCentreBySite(byte sitId);

        IEnumerable<QuadrangleCentreDto> GetCentreDtosBySite(byte siteId);
    }
}