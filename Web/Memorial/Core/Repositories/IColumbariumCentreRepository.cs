using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumCentreRepository : IRepository<ColumbariumCentre>
    {
        ColumbariumCentre GetActive(int id);

        IEnumerable<ColumbariumCentre> GetAllActive();

        IEnumerable<ColumbariumCentre> GetBySite(int siteId);
    }
}
