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
                .Where(m => m.Id == id && m.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Miscellaneous> GetAllActive()
        {
            return MemorialContext.Miscellaneous
                .Include(m => m.Site)
                .Where(m => m.DeleteDate == null)
                .ToList();
        }

        public IEnumerable<Miscellaneous> GetBySite(byte siteId)
        {
            return MemorialContext.Miscellaneous
                .Where(m => m.SiteId == siteId && m.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}