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

        void SetCentre(Core.Domain.ColumbariumCentre nicheCentre);

        int GetID();

        string GetName();

        string GetDescription();

        Core.Domain.ColumbariumCentre GetCentre();

        Core.Domain.ColumbariumCentre GetCentre(int id);

        ColumbariumCentreDto GetCentreDto();

        ColumbariumCentreDto GetCentreDto(int id);

        IEnumerable<ColumbariumCentreDto> GetCentreDtos();

        IEnumerable<Core.Domain.ColumbariumCentre> GetCentreBySite(int sitId);

        IEnumerable<ColumbariumCentreDto> GetCentreDtosBySite(int siteId);

        int Create(ColumbariumCentreDto columbariumCentreDto);

        bool Update(ColumbariumCentreDto columbariumCentreDto);

        bool Delete(int id);
    }
}