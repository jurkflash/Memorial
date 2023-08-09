using System.Collections.Generic;

namespace Memorial.Lib.SubProductService
{
    public interface ISubProductService
    {
        Core.Domain.SubProductService Get(int id);
        IEnumerable<Core.Domain.SubProductService> GetAll();
        IEnumerable<Core.Domain.SubProductService> GetByProduct(int productId);
        IEnumerable<Core.Domain.SubProductService> GetByProductIdAndOtherId(int productId, int otherId);
    }
}