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
                .Where(qa => qa.Id == id && qa.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<ColumbariumArea> GetByCentre(int centreId)
        {
            return MemorialContext.ColumbariumAreas
                .Where(qa => qa.ColumbariumCentreId == centreId
                && qa.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}