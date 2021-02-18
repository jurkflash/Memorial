using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestorRepository : Repository<Ancestor>, IAncestorRepository
    {
        public AncestorRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<Ancestor> GetByArea(int areaId)
        {
            return MemorialContext.Ancestors
                .Where(a => a.AncestorAreaId == areaId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}