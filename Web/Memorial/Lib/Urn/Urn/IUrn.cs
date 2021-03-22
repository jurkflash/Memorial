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

        IEnumerable<Core.Domain.Urn> GetUrnsBySite(byte siteId);

        IEnumerable<UrnDto> GetUrnDtosBySite(byte siteId);

        string GetName();

        string GetDescription();

        float GetPrice();
    }
}