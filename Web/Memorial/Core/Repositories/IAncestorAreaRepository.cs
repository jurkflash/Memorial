using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorAreaRepository : IRepository<AncestorArea>
    {
        AncestorArea GetActive(int id);

        IEnumerable<AncestorArea> GetAllActive();

        IEnumerable<AncestorArea> GetBySite(byte siteId);
    }
}
