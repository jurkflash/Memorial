using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestorAreaRepository : Repository<AncestorArea>, IAncestorAreaRepository
    {
        public AncestorAreaRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<AncestorArea> GetBySite(byte siteId)
        {
            return MemorialContext.AncestorAreas
                .Where(a => a.SiteId == siteId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}