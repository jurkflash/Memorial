using System.Collections.Generic;

namespace Memorial.Lib.Catalog
{
    public interface ICatalog
    {
        Core.Domain.Catalog Get(int id);
        IEnumerable<Core.Domain.Catalog> GetAll();
        IEnumerable<Core.Domain.Catalog> GetBySite(int siteId);
        IEnumerable<Core.Domain.Product> GetAvailableBySite(int siteId);
        int Add(Core.Domain.Catalog catalog);
        bool Remove(int id);
        IEnumerable<Core.Domain.Site> GetSitesAncestralTablet();
        IEnumerable<Core.Domain.Site> GetSitesCemetery();
        IEnumerable<Core.Domain.Site> GetSitesColumbarium();
        IEnumerable<Core.Domain.Site> GetSitesCremation();
        IEnumerable<Core.Domain.Site> GetSitesMiscellaneous();
        IEnumerable<Core.Domain.Site> GetSitesSpace();
        IEnumerable<Core.Domain.Site> GetSitesUrn();
    }
}