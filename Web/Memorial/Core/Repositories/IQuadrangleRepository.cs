using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleRepository : IRepository<Quadrangle>
    {
        Quadrangle GetActive(int id);

        IEnumerable<Quadrangle> GetByArea(int quadrangleAreaId);

        IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int quadrangleAreaId);
    }
}
