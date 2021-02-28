using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Site
{
    public interface ISite
    {
        void SetSite(int id);

        Core.Domain.Site GetSite();

        SiteDto GetSiteDto();

        Core.Domain.Site GetSite(byte id);

        SiteDto GetSiteDto(byte id);

        IEnumerable<Core.Domain.Site> GetSites();

        IEnumerable<SiteDto> GetSiteDtos();
    }
}