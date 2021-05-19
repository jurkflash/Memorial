using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumItemRepository : IRepository<ColumbariumItem>
    {
        ColumbariumItem GetActive(int id);

        IEnumerable<ColumbariumItem> GetByCentre(int columbariumCentreId);
    }
}
