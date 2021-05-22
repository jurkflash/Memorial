using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorRepository : IRepository<Ancestor>
    {
        Ancestor GetActive(int id);

        IEnumerable<Ancestor> GetByArea(int ancestralTabletAreaId);

        IEnumerable<Ancestor> GetAvailableByArea(int ancestralTabletAreaId);

        IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int ancestralTabletAreaId);
    }
}
