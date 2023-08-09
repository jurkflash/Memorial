using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICatalogRepository : IRepository<Catalog>
    {
        Catalog Get(int id);
        IEnumerable<Catalog> GetAllActive();
        IEnumerable<Catalog> GetBySite(int id);

        IEnumerable<Site> GetByProduct(int id);
    }
}
