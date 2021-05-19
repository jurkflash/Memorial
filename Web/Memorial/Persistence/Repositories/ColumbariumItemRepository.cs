using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class ColumbariumItemRepository : Repository<ColumbariumItem>, IColumbariumItemRepository
    {
        public ColumbariumItemRepository(MemorialContext context) : base(context)
        {
        }

        public ColumbariumItem GetActive(int id)
        {
            return MemorialContext.ColumbariumItems
                .Where(qi => qi.Id == id && qi.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<ColumbariumItem> GetByCentre(int columbariumCentreId)
        {
            return MemorialContext.ColumbariumItems
                .Where(qi => qi.ColumbariumCentreId == columbariumCentreId
                && qi.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}