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

        public AncestorArea GetActive(int id)
        {
            return MemorialContext.AncestorAreas
                .Where(aa => aa.Id == id && aa.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<AncestorArea> GetAllActive()
        {
            MemorialContext.Configuration.LazyLoadingEnabled = false;
            return MemorialContext.AncestorAreas
                .Where(aa => aa.DeleteDate == null)
                .ToList();
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