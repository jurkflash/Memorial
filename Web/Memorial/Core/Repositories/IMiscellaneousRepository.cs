using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IMiscellaneousRepository : IRepository<Miscellaneous>
    {
        IEnumerable<Miscellaneous> GetBySite(byte siteId);
    }
}
