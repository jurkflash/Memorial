using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ISpaceRepository : IRepository<Space>
    {
        Space GetActive(int id);

        IEnumerable<Space> GetAllActive();

        IEnumerable<Space> GetBySite(byte siteId);
    }
}
