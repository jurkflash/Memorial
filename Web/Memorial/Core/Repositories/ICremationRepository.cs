using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICremationRepository : IRepository<Cremation>
    {
        IEnumerable<Cremation> GetBySite(byte siteId);
    }
}
