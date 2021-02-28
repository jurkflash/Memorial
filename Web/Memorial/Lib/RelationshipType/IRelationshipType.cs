using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.RelationshipType
{
    public interface IRelationshipType
    {
        void SetRelationshipType(int id);

        Core.Domain.RelationshipType GetRelationshipType();

        RelationshipTypeDto GetRelationshipTypeDto();

        Core.Domain.RelationshipType GetRelationshipTypeById(int id);

        RelationshipTypeDto GetRelationshipTypeDtoById(int id);

        IEnumerable<Core.Domain.RelationshipType> GetRelationshipTypes();

        IEnumerable<RelationshipTypeDto> GetRelationshipTypeDtos();
    }
}