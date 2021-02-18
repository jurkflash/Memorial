using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IMiscellaneousItemRepository : IRepository<MiscellaneousItem>
    {
        IEnumerable<MiscellaneousItem> GetByMiscellaneous(int miscellaneousId);
    }
}
