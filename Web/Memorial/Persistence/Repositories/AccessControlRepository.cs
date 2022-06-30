using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AccessControlRepository : Repository<AccessControl>, IAccessControlRepository
    {
        public AccessControlRepository(MemorialContext context) : base(context)
        {
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}