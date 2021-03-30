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

        public PlotItem GetActive(int id)
        {
            return MemorialContext.PlotItems
                .Where(pi => pi.Id == id && pi.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<PlotItem> GetByPlot(int plotId)
        {
            return MemorialContext.PlotItems
                .Where(pi => pi.PlotId == plotId
                && pi.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}