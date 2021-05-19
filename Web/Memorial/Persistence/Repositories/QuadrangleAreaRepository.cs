using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleAreaRepository : Repository<QuadrangleArea>, IQuadrangleAreaRepository
    {
        public QuadrangleAreaRepository(MemorialContext context) : base(context)
        {
        }

        public QuadrangleArea GetActive(int id)
        {
            return MemorialContext.QuadrangleAreas
                .Where(qa => qa.Id == id && qa.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<QuadrangleArea> GetByCentre(int centreId)
        {
            return MemorialContext.QuadrangleAreas
                .Where(qa => qa.ColumbariumCentreId == centreId
                && qa.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}