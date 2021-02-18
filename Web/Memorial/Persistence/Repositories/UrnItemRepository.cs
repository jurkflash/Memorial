using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class UrnItemRepository : Repository<UrnItem>, IUrnItemRepository
    {
        public UrnItemRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<UrnItem> GetByUrn(int urnId)
        {
            return MemorialContext.UrnItems
                .Where(ui => ui.Id == urnId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}