using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IUrnItemRepository : IRepository<UrnItem>
    {
        UrnItem GetActive(int id);

        IEnumerable<UrnItem> GetByUrn(int urnId);
    }
}
