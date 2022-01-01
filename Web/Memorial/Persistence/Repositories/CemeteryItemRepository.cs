using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CemeteryItemRepository : Repository<CemeteryItem>, ICemeteryItemRepository
    {
        public CemeteryItemRepository(MemorialContext context) : base(context)
        {
        }

        public CemeteryItem GetActive(int id)
        {
            return MemorialContext.CemeteryItems
                .Include(ci => ci.SubProductService)
                .Where(pi => pi.Id == id && pi.DeletedDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<CemeteryItem> GetByPlot(int plotId)
        {
            return MemorialContext.CemeteryItems
                .Include(ci => ci.SubProductService)
                .Where(pi => pi.PlotId == plotId
                && pi.DeletedDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}