using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class ColumbariumAreaRepository : Repository<ColumbariumArea>, IColumbariumAreaRepository
    {
        public ColumbariumAreaRepository(MemorialContext context) : base(context)
        {
        }

        public ColumbariumArea GetActive(int id)
        {
            return MemorialContext.ColumbariumAreas
                .Include(qa => qa.ColumbariumCentre)
                .Where(qa => qa.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<ColumbariumArea> GetByCentre(int centreId)
        {
            return MemorialContext.ColumbariumAreas
                .Include(qa => qa.ColumbariumCentre)
                .Where(qa => qa.ColumbariumCentreId == centreId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}