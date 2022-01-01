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

        public Cremation GetActive(int id)
        {
            return MemorialContext.Cremations
                .Include(c => c.Site)
                .Where(c => c.Id == id && c.DeletedDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Cremation> GetAllActive()
        {
            return MemorialContext.Cremations
                .Include(c => c.Site)
                .Where(c => c.DeletedDate == null)
                .ToList();
        }

        public IEnumerable<Cremation> GetBySite(int siteId)
        {
            return MemorialContext.Cremations
                .Where(c => c.SiteId == siteId && c.DeletedDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}