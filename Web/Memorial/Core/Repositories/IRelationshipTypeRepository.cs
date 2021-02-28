using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IRelationshipTypeRepository : IRepository<RelationshipType>
    {
        RelationshipType GetActive(int id);

        IEnumerable<RelationshipType> GetAllActive();
    }
}
