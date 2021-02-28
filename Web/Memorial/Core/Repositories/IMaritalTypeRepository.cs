using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IMaritalTypeRepository : IRepository<MaritalType>
    {
        MaritalType GetActive(int id);

        IEnumerable<MaritalType> GetAllActive();
    }
}
