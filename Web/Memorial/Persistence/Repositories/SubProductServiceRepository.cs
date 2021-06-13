using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class SubProductServiceRepository : Repository<SubProductService>, ISubProductServiceRepository
    {
        public SubProductServiceRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<SubProductService> GetSubProductServicesByProductId(int productId)
        {
            return MemorialContext.SubProductServices
                .Include(sp => sp.Product)
                .Where(sp => sp.ProductId == productId)
                .ToList();
        }

        public IEnumerable<SubProductService> GetSubProductServicesByProductIdAndOtherId(int productId, int otherId)
        {
            return MemorialContext.SubProductServices
                .Include(sp => sp.Product)
                .Where(sp => sp.ProductId == productId && sp.OtherId == otherId)
                .ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}