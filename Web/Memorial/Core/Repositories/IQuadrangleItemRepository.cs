using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleItemRepository : IRepository<QuadrangleItem>
    {
        QuadrangleItem GetActive(int id);

        IEnumerable<QuadrangleItem> GetByCentre(int quadrangleCentreId);
    }
}
