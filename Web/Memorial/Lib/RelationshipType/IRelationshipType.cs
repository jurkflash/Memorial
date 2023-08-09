using System.Collections.Generic;

namespace Memorial.Lib.RelationshipType
{
    public interface IRelationshipType
    {
        IEnumerable<Core.Domain.RelationshipType> GetAll();
    }
}