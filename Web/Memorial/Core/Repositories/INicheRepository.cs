using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface INicheRepository : IRepository<Niche>
    {
        Niche GetActive(int id);

        IEnumerable<Niche> GetByArea(int columbariumAreaId);

        IEnumerable<Niche> GetAvailableByArea(int columbariumAreaId);

        IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int columbariumAreaId);
    }
}
