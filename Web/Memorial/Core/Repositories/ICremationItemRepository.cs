using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICremationItemRepository : IRepository<CremationItem>
    {
        IEnumerable<CremationItem> GetByCremation(int urnId);
    }
}
