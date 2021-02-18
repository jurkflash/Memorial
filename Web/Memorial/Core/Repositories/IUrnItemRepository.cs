using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IUrnItemRepository : IRepository<UrnItem>
    {
        IEnumerable<UrnItem> GetByUrn(int urnId);
    }
}
