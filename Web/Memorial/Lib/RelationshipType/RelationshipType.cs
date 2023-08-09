using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.RelationshipType
{
    public class RelationshipType : IRelationshipType
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelationshipType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Core.Domain.RelationshipType> GetAll()
        {
            return _unitOfWork.RelationshipTypes.GetAllActive();
        }
    }
}