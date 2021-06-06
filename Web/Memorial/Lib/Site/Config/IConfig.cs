using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Site
{
    public interface IConfig
    {
        byte CreateSite(SiteDto siteDto);
        bool DeleteSite(byte id);
        SiteDto GetSiteDto(byte id);
        IEnumerable<SiteDto> GetSiteDtos();
        bool UpdateSite(SiteDto siteDto);
    }
}