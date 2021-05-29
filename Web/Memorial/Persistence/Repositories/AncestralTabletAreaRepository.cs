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
                .Where(aa => aa.Id == id && aa.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<AncestralTabletArea> GetAllActive()
        {
            MemorialContext.Configuration.LazyLoadingEnabled = false;
            return MemorialContext.AncestralTabletAreas
                .Include(at => at.Site)
                .Where(aa => aa.DeleteDate == null)
                .ToList();
        }

        public IEnumerable<AncestralTabletArea> GetBySite(byte siteId)
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