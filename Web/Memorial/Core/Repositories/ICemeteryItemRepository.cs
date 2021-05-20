using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICemeteryItemRepository : IRepository<CemeteryItem>
    {
        CemeteryItem GetActive(int id);

        IEnumerable<CemeteryItem> GetByPlot(int plotId);
    }
}
