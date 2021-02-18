using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ISpaceRepository : IRepository<Space>
    {
        IEnumerable<Space> GetBySite(byte siteId);
    }
}
