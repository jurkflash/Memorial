using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ISiteRepository : IRepository<Site>
    {
        Site GetActive(int id);

        IEnumerable<Site> GetAllActive();
    }
}
