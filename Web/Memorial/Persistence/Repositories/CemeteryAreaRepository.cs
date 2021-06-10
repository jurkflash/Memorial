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
                .Where(pa => pa.Id == id && pa.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<CemeteryArea> GetAllActive()
        {
            return MemorialContext.CemeteryAreas
                .Include(pa => pa.Site)
                .Where(pa => pa.DeleteDate == null)
                .ToList();
        }

        public IEnumerable<CemeteryArea> GetBySite(int siteId)
        {
            return MemorialContext.CemeteryAreas
                .Where(pa => pa.SiteId == siteId
                && pa.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}