using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class MiscellaneousRepository : Repository<Miscellaneous>, IMiscellaneousRepository
    {
        public MiscellaneousRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<Miscellaneous> GetBySite(byte siteId)
        {
            return MemorialContext.Miscellaneous
                .Where(m => m.SiteId == siteId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}