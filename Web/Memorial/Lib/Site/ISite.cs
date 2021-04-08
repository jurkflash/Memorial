using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Site
{
    public interface ISite
    {
        bool CreateSite(SiteDto siteDto);
        bool DeleteSite(byte id);
        Core.Domain.Site GetSite();
        Core.Domain.Site GetSite(byte id);
        SiteDto GetSiteDto();
        SiteDto GetSiteDto(byte id);
        IEnumerable<SiteDto> GetSiteDtos();
        IEnumerable<Core.Domain.Site> GetSites();
        void SetSite(int id);
        bool UpdateSite(SiteDto siteDto);
    }
}