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
                .Where(rt => rt.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<RelationshipType> GetAllActive()
        {
            return MemorialContext.RelationshipTypes.ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}