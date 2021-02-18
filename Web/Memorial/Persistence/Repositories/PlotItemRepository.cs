using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class PlotItemRepository : Repository<PlotItem>, IPlotItemRepository
    {
        public PlotItemRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<PlotItem> GetByArea(int plotAreaId)
        {
            return MemorialContext.PlotItems
                .Where(pi => pi.PlotAreaId == plotAreaId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}