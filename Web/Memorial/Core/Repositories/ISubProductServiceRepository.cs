using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ISubProductServiceRepository : IRepository<SubProductService>
    {
        IEnumerable<SubProductService> GetSubProductServicesByProduct(int productId);
    }
}
