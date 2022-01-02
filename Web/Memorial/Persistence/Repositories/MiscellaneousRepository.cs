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

        public Miscellaneous GetActive(int id)
        {
            return MemorialContext.Miscellaneous
                .Include(m => m.Site)
                .Where(m => m.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<Miscellaneous> GetAllActive()
        {
            return MemorialContext.Miscellaneous
                .Include(m => m.Site)
                .ToList();
        }

        public IEnumerable<Miscellaneous> GetBySite(int siteId)
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