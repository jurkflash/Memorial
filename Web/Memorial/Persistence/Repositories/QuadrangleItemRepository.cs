using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleItemRepository : Repository<QuadrangleItem>, IQuadrangleItemRepository
    {
        public QuadrangleItemRepository(MemorialContext context) : base(context)
        {
        }

        public QuadrangleItem GetActive(int id)
        {
            return MemorialContext.QuadrangleItems
                .Where(qi => qi.Id == id && qi.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<QuadrangleItem> GetByCentre(int quadrangleCentreId)
        {
            return MemorialContext.QuadrangleItems
                .Where(qi => qi.QuadrangleCentreId == quadrangleCentreId
                && qi.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}