using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.RelationshipType
{
    public class RelationshipType : IRelationshipType
    {
        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.RelationshipType _relationshipType;

        public RelationshipType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetRelationshipType(int id)
        {
            _relationshipType = _unitOfWork.RelationshipTypes.GetActive(id);
        }

        public Core.Domain.RelationshipType GetRelationshipType()
        {
            return _relationshipType;
        }

        public RelationshipTypeDto GetRelationshipTypeDto()
        {
            return Mapper.Map<Core.Domain.RelationshipType, RelationshipTypeDto>(GetRelationshipType());
        }

        public Core.Domain.RelationshipType GetRelationshipTypeById(int id)
        {
            return _unitOfWork.RelationshipTypes.GetActive(id);
        }

        public RelationshipTypeDto GetRelationshipTypeDtoById(int id)
        {
            return Mapper.Map<Core.Domain.RelationshipType, RelationshipTypeDto>(GetRelationshipTypeById(id));
        }

        public IEnumerable<Core.Domain.RelationshipType> GetRelationshipTypes()
        {
            return _unitOfWork.RelationshipTypes.GetAllActive();
        }

        public IEnumerable<RelationshipTypeDto> GetRelationshipTypeDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.RelationshipType>, IEnumerable<RelationshipTypeDto>>(GetRelationshipTypes());
        }

    }
}