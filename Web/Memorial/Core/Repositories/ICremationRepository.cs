using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICremationRepository : IRepository<Cremation>
    {
        Cremation GetActive(int id);

        IEnumerable<Cremation> GetAllActive();

        IEnumerable<Cremation> GetBySite(int siteId);
    }
}
