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

        public IEnumerable<Space> GetBySite(byte siteId)
        {
            return MemorialContext.Spaces
                .Where(s => s.SiteId == siteId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}