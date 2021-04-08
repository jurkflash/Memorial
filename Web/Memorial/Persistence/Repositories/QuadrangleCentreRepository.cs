using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleCentreRepository : Repository<QuadrangleCentre>, IQuadrangleCentreRepository
    {
        public QuadrangleCentreRepository(MemorialContext context) : base(context)
        {
        }

        public QuadrangleCentre GetActive(int id)
        {
            return MemorialContext.QuadrangleCentres
                .Include(qc => qc.Site)
                .Where(qc => qc.Id == id && qc.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<QuadrangleCentre> GetAllActive()
        {
            return MemorialContext.QuadrangleCentres
                .Include(qc => qc.Site)
                .Where(qc => qc.DeleteDate == null)
                .ToList();
        }

        public IEnumerable<QuadrangleCentre> GetBySite(byte siteId)
        {
            return MemorialContext.QuadrangleCentres
                .Where(qc => qc.SiteId == siteId
                && qc.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}