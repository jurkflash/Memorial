using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IUrnRepository : IRepository<Urn>
    {
        IEnumerable<Urn> GetBySite(byte siteId);
    }
}
