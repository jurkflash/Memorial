using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IMiscellaneousItemRepository : IRepository<MiscellaneousItem>
    {
        MiscellaneousItem GetActive(int id);

        IEnumerable<MiscellaneousItem> GetByMiscellaneous(int miscellaneousId);
    }
}
