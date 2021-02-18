using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CatalogRepository : Repository<Catalog>, ICatalogRepository
    {
        public CatalogRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<Catalog> GetBySite(int id)
        {
            return MemorialContext.Catalogs.Where(c => c.SiteId == id).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}