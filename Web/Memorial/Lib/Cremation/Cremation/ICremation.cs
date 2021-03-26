using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cremation
{
    public interface ICremation
    {
        void SetCremation(int id);

        Core.Domain.Cremation GetCremation();

        CremationDto GetCremationDto();

        Core.Domain.Cremation GetCremation(int id);

        CremationDto GetCremationDto(int id);

        IEnumerable<Core.Domain.Cremation> GetCremationBySite(byte siteId);

        IEnumerable<CremationDto> GetCremationDtosBySite(byte siteId);

        string GetName();

        string GetDescription();
    }
}