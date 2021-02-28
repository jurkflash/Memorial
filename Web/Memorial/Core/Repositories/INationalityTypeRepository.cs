using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface INationalityTypeRepository : IRepository<NationalityType>
    {
        NationalityType GetActive(int id);

        IEnumerable<NationalityType> GetAllActive();
    }
}
