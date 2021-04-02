using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IMiscellaneousItemRepository : IRepository<MiscellaneousItem>
    {
        MiscellaneousItem GetActive(int id);

        IEnumerable<MiscellaneousItem> GetAllActive();
        IEnumerable<MiscellaneousItem> GetByMiscellaneous(int miscellaneousId);
    }
}
