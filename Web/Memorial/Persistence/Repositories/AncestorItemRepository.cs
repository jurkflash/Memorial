using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestorItemRepository : Repository<AncestorItem>, IAncestorItemRepository
    {
        public AncestorItemRepository(MemorialContext context) : base(context)
        {
        }

        public AncestorItem GetActive(int id)
        {
            return MemorialContext.AncestorItems
                .Where(ai => ai.Id == id && ai.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<AncestorItem> GetAllActive()
        {
            MemorialContext.Configuration.LazyLoadingEnabled = false;
            return MemorialContext.AncestorItems
                .Where(ai => ai.DeleteDate == null)
                .ToList();
        }

        public IEnumerable<AncestorItem> GetByArea(int areaId)
        {
            return MemorialContext.AncestorItems
                .Where(ai => ai.AncestralTabletAreaId == areaId
                && ai.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}