using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICatalogRepository : IRepository<Catalog>
    {
        IEnumerable<Catalog> GetBySite(int id);
    }
}
