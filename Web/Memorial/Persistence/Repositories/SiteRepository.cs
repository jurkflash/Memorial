using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class SiteRepository : Repository<Site>, ISiteRepository
    {
        public SiteRepository(MemorialContext context) : base(context)
        {
        }

        public Site GetActive(int id)
        {
            return MemorialContext.Sites
                .Where(s => s.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<Site> GetAllActive()
        {
            return MemorialContext.Sites.ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}