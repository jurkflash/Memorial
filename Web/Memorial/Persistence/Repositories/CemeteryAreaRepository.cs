using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CemeteryAreaRepository : Repository<CemeteryArea>, ICemeteryAreaRepository
    {
        public CemeteryAreaRepository(MemorialContext context) : base(context)
        {
        }

        public CemeteryArea GetActive(int id)
        {
            return MemorialContext.CemeteryAreas
                .Include(ca => ca.Site)
                .Where(ca => ca.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<CemeteryArea> GetAllActive()
        {
            return MemorialContext.CemeteryAreas
                .Include(ca => ca.Site)
                .ToList();
        }

        public IEnumerable<CemeteryArea> GetBySite(int siteId)
        {
            return MemorialContext.CemeteryAreas
                .Where(ca => ca.SiteId == siteId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}