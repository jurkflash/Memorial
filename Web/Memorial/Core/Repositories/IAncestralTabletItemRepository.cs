using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestralTabletItemRepository : IRepository<AncestralTabletItem>
    {
        AncestralTabletItem GetActive(int id);

        IEnumerable<AncestralTabletItem> GetAllActive();

        IEnumerable<AncestralTabletItem> GetByArea(int areaId);
    }
}
