using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ISpaceItemRepository : IRepository<SpaceItem>
    {
        IEnumerable<SpaceItem> GetBySpace(int spaceId);
    }
}
