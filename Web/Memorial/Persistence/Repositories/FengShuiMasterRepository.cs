using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class FengShuiMasterRepository : Repository<FengShuiMaster>, IFengShuiMasterRepository
    {
        public FengShuiMasterRepository(MemorialContext context) : base(context)
        {
        }

        public FengShuiMaster GetActive(int id)
        {
            return MemorialContext.FengShuiMasters
                .Where(fs => fs.Id == id).FirstOrDefault();
        }

        public IEnumerable<FengShuiMaster> GetAllActive()
        {
            return MemorialContext.FengShuiMasters.ToList();
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}