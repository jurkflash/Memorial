using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IReligionTypeRepository : IRepository<ReligionType>
    {
        ReligionType GetActive(int id);

        IEnumerable<ReligionType> GetAllActive();
    }
}
