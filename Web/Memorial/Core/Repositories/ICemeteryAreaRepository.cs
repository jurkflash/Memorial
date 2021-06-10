using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICemeteryAreaRepository : IRepository<CemeteryArea>
    {
        CemeteryArea GetActive(int id);

        IEnumerable<CemeteryArea> GetAllActive();

        IEnumerable<CemeteryArea> GetBySite(int siteId);
    }
}
