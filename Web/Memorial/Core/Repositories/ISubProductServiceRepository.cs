using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ISubProductServiceRepository : IRepository<SubProductService>
    {
        IEnumerable<SubProductService> GetSubProductServicesByProductId(int productId);

        IEnumerable<SubProductService> GetSubProductServicesByProductIdAndOtherId(int productId, int otherId);
    }
}
