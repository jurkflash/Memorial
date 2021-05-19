using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class ColumbariumCentreRepository : Repository<ColumbariumCentre>, IColumbariumCentreRepository
    {
        public ColumbariumCentreRepository(MemorialContext context) : base(context)
        {
        }

        public ColumbariumCentre GetActive(int id)
        {
            return MemorialContext.ColumbariumCentres
                .Include(qc => qc.Site)
                .Where(qc => qc.Id == id && qc.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<ColumbariumCentre> GetAllActive()
        {
            return MemorialContext.ColumbariumCentres
                .Include(qc => qc.Site)
                .Where(qc => qc.DeleteDate == null)
                .ToList();
        }

        public IEnumerable<ColumbariumCentre> GetBySite(byte siteId)
        {
            return MemorialContext.ColumbariumCentres
                .Where(qc => qc.SiteId == siteId
                && qc.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}