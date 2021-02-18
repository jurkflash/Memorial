using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class SpaceItemRepository : Repository<SpaceItem>, ISpaceItemRepository
    {
        public SpaceItemRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<SpaceItem> GetBySpace(int spaceId)
        {
            return MemorialContext.SpaceItems
                .Where(si => si.SpaceId == spaceId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}