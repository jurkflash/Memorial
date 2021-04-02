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
                .Where(s => s.Id == id && s.DeleteDate == null).SingleOrDefault();
        }

        public IEnumerable<Space> GetAllActive()
        {
            return MemorialContext.Spaces
                .Include(s => s.Site)
                .Where(s => s.DeleteDate == null).ToList();
        }

        public IEnumerable<Space> GetBySite(byte siteId)
        {
            return MemorialContext.Spaces
                .Where(s => s.SiteId == siteId && s.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}