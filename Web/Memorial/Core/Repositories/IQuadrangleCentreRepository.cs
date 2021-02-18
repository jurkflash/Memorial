using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleCentreRepository : IRepository<QuadrangleCentre>
    {
        QuadrangleCentre GetActive(int id);

        IEnumerable<QuadrangleCentre> GetBySite(byte siteId);
    }
}
