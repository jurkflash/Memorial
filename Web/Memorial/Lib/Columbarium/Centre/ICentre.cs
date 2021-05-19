using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
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

        IEnumerable<QuadrangleCentreDto> GetCentreDtos();

        IEnumerable<Core.Domain.QuadrangleCentre> GetCentreBySite(byte sitId);

        IEnumerable<QuadrangleCentreDto> GetCentreDtosBySite(byte siteId);

        bool Create(QuadrangleCentreDto quadrangleCentreDto);

        bool Update(Core.Domain.QuadrangleCentre quadrangleCentre);

        bool Delete(int id);
    }
}