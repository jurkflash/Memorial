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
                .Where(m => m.Id == id && m.DeletedDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Miscellaneous> GetAllActive()
        {
            return MemorialContext.Miscellaneous
                .Include(m => m.Site)
                .Where(m => m.DeletedDate == null)
                .ToList();
        }

        public IEnumerable<Miscellaneous> GetBySite(int siteId)
        {
            return MemorialContext.Miscellaneous
                .Where(m => m.SiteId == siteId && m.DeletedDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}