using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestralTabletAreaRepository : Repository<AncestralTabletArea>, IAncestralTabletAreaRepository
    {
        public AncestralTabletAreaRepository(MemorialContext context) : base(context)
        {
        }

        public AncestralTabletArea GetActive(int id)
        {
            return MemorialContext.AncestralTabletAreas
                .Include(at => at.Site)
                .Where(aa => aa.Id == id && aa.DeletedDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<AncestralTabletArea> GetAllActive()
        {
            MemorialContext.Configuration.LazyLoadingEnabled = false;
            return MemorialContext.AncestralTabletAreas
                .Include(at => at.Site)
                .Where(aa => aa.DeletedDate == null)
                .ToList();
        }

        public IEnumerable<AncestralTabletArea> GetBySite(int siteId)
        {
            return MemorialContext.AncestralTabletAreas
                .Where(a => a.SiteId == siteId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}