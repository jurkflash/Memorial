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
                .Where(qc => qc.Id == id && qc.DeletedDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<ColumbariumCentre> GetAllActive()
        {
            return MemorialContext.ColumbariumCentres
                .Include(qc => qc.Site)
                .Where(qc => qc.DeletedDate == null)
                .ToList();
        }

        public IEnumerable<ColumbariumCentre> GetBySite(int siteId)
        {
            return MemorialContext.ColumbariumCentres
                .Where(qc => qc.SiteId == siteId
                && qc.DeletedDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}