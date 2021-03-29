using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorItemRepository : IRepository<AncestorItem>
    {
        AncestorItem GetActive(int id);

        IEnumerable<AncestorItem> GetByArea(int areaId);
    }
}
