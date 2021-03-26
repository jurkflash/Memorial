using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CremationItemRepository : Repository<CremationItem>, ICremationItemRepository
    {
        public CremationItemRepository(MemorialContext context) : base(context)
        {
        }

        public CremationItem GetActive(int id)
        {
            return MemorialContext.CremationItems
                .Where(ci => ci.Id == id && ci.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<CremationItem> GetByCremation(int cremationId)
        {
            return MemorialContext.CremationItems
                .Where(ci => ci.Id == cremationId && ci.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}