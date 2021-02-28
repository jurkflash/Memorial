using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IGenderTypeRepository : IRepository<GenderType>
    {
        GenderType GetActive(int id);

        IEnumerable<GenderType> GetAllActive();
    }
}
