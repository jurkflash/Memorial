using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class MiscellaneousItemRepository : Repository<MiscellaneousItem>, IMiscellaneousItemRepository
    {
        public MiscellaneousItemRepository(MemorialContext context) : base(context)
        {
        }

        public MiscellaneousItem GetActive(int id)
        {
            return MemorialContext.MiscellaneousItems
                .Include(mi => mi.SubProductService)
                .Include(mi => mi.Miscellaneous)
                .Include(mi => mi.Miscellaneous.Site)
                .Where(mi => mi.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<MiscellaneousItem> GetAllActive()
        {
            return MemorialContext.MiscellaneousItems
                .Include(mi => mi.SubProductService)
                .ToList();
        }

        public IEnumerable<MiscellaneousItem> GetByMiscellaneous(int miscellaneousId)
        {
            return MemorialContext.MiscellaneousItems
                .Include(mi => mi.SubProductService)
                .Where(mi => mi.MiscellaneousId == miscellaneousId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}