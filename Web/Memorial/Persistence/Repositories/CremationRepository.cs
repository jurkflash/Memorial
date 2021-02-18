using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CremationRepository : Repository<Cremation>, ICremationRepository
    {
        public CremationRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<Cremation> GetBySite(byte siteId)
        {
            return MemorialContext.Cremations
                .Where(c => c.SiteId == siteId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}