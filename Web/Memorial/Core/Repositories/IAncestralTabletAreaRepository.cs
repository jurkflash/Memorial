using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestralTabletAreaRepository : IRepository<AncestralTabletArea>
    {
        AncestralTabletArea GetActive(int id);

        IEnumerable<AncestralTabletArea> GetAllActive();

        IEnumerable<AncestralTabletArea> GetBySite(int siteId);
    }
}
