using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class RelationshipTypeRepository : Repository<RelationshipType>, IRelationshipTypeRepository
    {
        public RelationshipTypeRepository(MemorialContext context) : base(context)
        {
        }

        public RelationshipType GetActive(int id)
        {
            return MemorialContext.RelationshipTypes
                .Where(rt => rt.Id == id && rt.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<RelationshipType> GetAllActive()
        {
            return MemorialContext.RelationshipTypes
                .Where(rt => rt.DeleteDate == null);
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}