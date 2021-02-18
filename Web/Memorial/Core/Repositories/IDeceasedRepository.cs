using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IDeceasedRepository : IRepository<Deceased>
    {
        Deceased GetByIC(string IC);

        Deceased GetActive(int id);

        IEnumerable<Deceased> GetByApplicant(int id);
    }
}
