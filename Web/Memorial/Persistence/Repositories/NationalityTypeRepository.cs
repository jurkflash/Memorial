using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class NationalityTypeRepository : Repository<NationalityType>, INationalityTypeRepository
    {
        public NationalityTypeRepository(MemorialContext context) : base(context)
        {
        }

        public NationalityType GetActive(int id)
        {
            return MemorialContext.NationalityTypes
                .Where(nt => nt.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<NationalityType> GetAllActive()
        {
            return MemorialContext.NationalityTypes.ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}