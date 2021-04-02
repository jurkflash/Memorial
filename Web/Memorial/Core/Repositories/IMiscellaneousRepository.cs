using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IMiscellaneousRepository : IRepository<Miscellaneous>
    {
        Miscellaneous GetActive(int id);

        IEnumerable<Miscellaneous> GetAllActive();

        IEnumerable<Miscellaneous> GetBySite(byte siteId);
    }
}
