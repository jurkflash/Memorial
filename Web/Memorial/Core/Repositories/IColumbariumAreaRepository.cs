using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumAreaRepository : IRepository<ColumbariumArea>
    {
        ColumbariumArea GetActive(int id);

        IEnumerable<ColumbariumArea> GetByCentre(int centreId);
    }
}
