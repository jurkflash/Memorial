using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleAreaRepository : IRepository<QuadrangleArea>
    {
        QuadrangleArea GetActive(int id);

        IEnumerable<QuadrangleArea> GetByCentre(int centreId);
    }
}
