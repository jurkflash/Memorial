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

        public PlotArea GetActive(int id)
        {
            return MemorialContext.PlotAreas
                .Where(pa => pa.Id == id && pa.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<PlotArea> GetBySite(byte siteId)
        {
            return MemorialContext.PlotAreas
                .Where(pa => pa.SiteId == siteId
                && pa.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}