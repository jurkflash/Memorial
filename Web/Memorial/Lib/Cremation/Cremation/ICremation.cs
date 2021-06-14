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

        IEnumerable<CremationDto> GetCremationDtos();

        Core.Domain.Cremation GetCremation(int id);

        CremationDto GetCremationDto(int id);

        IEnumerable<Core.Domain.Cremation> GetCremationBySite(int siteId);

        IEnumerable<CremationDto> GetCremationDtosBySite(int siteId);

        string GetName();

        string GetDescription();

        int Create(CremationDto cremationDto);

        bool Update(CremationDto cremationDto);

        bool Delete(int id);
    }
}