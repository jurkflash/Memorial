using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class UrnRepository : Repository<Urn>, IUrnRepository
    {
        public UrnRepository(MemorialContext context) : base(context)
        {
        }

        public Urn GetActive(int id)
        {
            return MemorialContext.Urns
                .Include(u => u.Site)
                .Where(u => u.Id == id && u.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Urn> GetBySite(byte siteId)
        {
            return MemorialContext.Urns
                .Where(u => u.SiteId == siteId && u.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}