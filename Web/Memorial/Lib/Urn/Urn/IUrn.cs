using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Urn
{
    public interface IUrn
    {
        void SetUrn(int id);

        Core.Domain.Urn GetUrn();

        UrnDto GetUrnDto();

        Core.Domain.Urn GetUrn(int id);

        UrnDto GetUrnDto(int id);

        IEnumerable<UrnDto> GetUrnDtos();

        IEnumerable<Core.Domain.Urn> GetUrnsBySite(int siteId);

        IEnumerable<UrnDto> GetUrnDtosBySite(int siteId);

        string GetName();

        string GetDescription();

        float GetPrice();

        int Create(UrnDto urnDto);

        bool Update(UrnDto urnDto);

        bool Delete(int id);
    }
}