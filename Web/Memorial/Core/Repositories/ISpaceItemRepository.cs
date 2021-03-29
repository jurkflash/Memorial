using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ISpaceItemRepository : IRepository<SpaceItem>
    {
        SpaceItem GetActive(int id);

        IEnumerable<SpaceItem> GetBySpace(int spaceId);
    }
}
