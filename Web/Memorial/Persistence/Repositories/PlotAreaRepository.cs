using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class PlotAreaRepository : Repository<PlotArea>, IPlotAreaRepository
    {
        public PlotAreaRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<PlotArea> GetBySite(byte siteId)
        {
            return MemorialContext.PlotAreas
                .Where(pa => pa.SiteId == siteId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}