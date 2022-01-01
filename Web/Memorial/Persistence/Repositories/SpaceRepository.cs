using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class SpaceRepository : Repository<Space>, ISpaceRepository
    {
        public SpaceRepository(MemorialContext context) : base(context)
        {
        }

        public Space GetActive(int id)
        {
            return MemorialContext.Spaces
                .Include(s => s.Site)
                .Where(s => s.Id == id && s.DeletedDate == null).SingleOrDefault();
        }

        public IEnumerable<Space> GetAllActive()
        {
            return MemorialContext.Spaces
                .Include(s => s.Site)
                .Where(s => s.DeletedDate == null).ToList();
        }

        public IEnumerable<Space> GetBySite(int siteId)
        {
            return MemorialContext.Spaces
                .Where(s => s.SiteId == siteId && s.DeletedDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}