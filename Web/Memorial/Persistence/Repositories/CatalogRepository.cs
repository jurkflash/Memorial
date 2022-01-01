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

        public Catalog GetActive(int id)
        {
            return MemorialContext.Catalogs
                .Include(c=>c.Product)
                .Where(c => c.Id == id && c.DeletedDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Catalog> GetAllActive()
        {
            return MemorialContext.Catalogs
                .Include(c => c.Product)
                .Where(c => c.DeletedDate == null);
        }

        public IEnumerable<Catalog> GetBySite(int id)
        {
            return MemorialContext.Catalogs
                .Include(c => c.Product)
                .Where(c => c.SiteId == id && c.DeletedDate == null).ToList();
        }

        public IEnumerable<Site> GetByProduct(int id)
        {
            return MemorialContext.Catalogs
                .Include(c => c.Site)
                .Where(c => c.ProductId == id && c.DeletedDate == null)
                .Select(c => c.Site)
                .ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}