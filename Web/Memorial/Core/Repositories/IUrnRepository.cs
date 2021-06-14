using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IUrnRepository : IRepository<Urn>
    {
        Urn GetActive(int id);

        IEnumerable<Urn> GetAllActive();

        IEnumerable<Urn> GetBySite(int siteId);
    }
}
