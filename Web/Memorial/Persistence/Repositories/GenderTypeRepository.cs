using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class GenderTypeRepository : Repository<GenderType>, IGenderTypeRepository
    {
        public GenderTypeRepository(MemorialContext context) : base(context)
        {
        }

        public GenderType GetActive(int id)
        {
            return MemorialContext.GenderTypes
                .Where(gt => gt.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<GenderType> GetAllActive()
        {
            return MemorialContext.GenderTypes.ToList();
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}