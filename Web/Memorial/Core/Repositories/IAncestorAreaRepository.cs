using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorAreaRepository : IRepository<AncestorArea>
    {
        IEnumerable<AncestorArea> GetBySite(byte siteId);
    }
}
