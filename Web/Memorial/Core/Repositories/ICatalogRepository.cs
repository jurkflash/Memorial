using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICatalogRepository : IRepository<Catalog>
    {
        IEnumerable<Catalog> GetAllActive();
        IEnumerable<Catalog> GetBySite(int id);
        IEnumerable<Site> GetByProduct(int id);
    }
}
