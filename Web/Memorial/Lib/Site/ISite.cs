using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Site
{
    public interface ISite
    {
        int CreateSite(SiteDto siteDto);
        bool DeleteSite(int id);
        Core.Domain.Site GetSite();
        Core.Domain.Site GetSite(int id);
        SiteDto GetSiteDto();
        SiteDto GetSiteDto(int id);
        IEnumerable<SiteDto> GetSiteDtos();
        IEnumerable<Core.Domain.Site> GetSites();
        void SetSite(int id);
        bool UpdateSite(SiteDto siteDto);
    }
}