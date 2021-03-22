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
                .Where(mi => mi.Id == id && mi.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<MiscellaneousItem> GetByMiscellaneous(int miscellaneousId)
        {
            return MemorialContext.MiscellaneousItems
                .Where(mi => mi.Id == miscellaneousId && mi.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}