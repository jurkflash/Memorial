using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class GenderTypeRepository : Repository<GenderType>, IGenderTypeRepository
    {
        public GenderTypeRepository(MemorialContext context) : base(context)
        {
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}