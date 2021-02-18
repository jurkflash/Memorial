using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorRepository : IRepository<Ancestor>
    {
        IEnumerable<Ancestor> GetByArea(int areaId);
    }
}
