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

        public override Catalog Get(int id)
        {
            return MemorialContext.Catalogs
                .Include(c=>c.Product)
                .Where(c => c.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<Catalog> GetAllActive()
        {
            return MemorialContext.Catalogs
                .Include(c => c.Product);
        }

        public IEnumerable<Catalog> GetBySite(int id)
        {
            return MemorialContext.Catalogs
                .Include(c => c.Product)
                .Where(c => c.SiteId == id).ToList();
        }

        public IEnumerable<Site> GetByProduct(int id)
        {
            return MemorialContext.Catalogs
                .Include(c => c.Site)
                .Where(c => c.ProductId == id)
                .Select(c => c.Site)
                .ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}