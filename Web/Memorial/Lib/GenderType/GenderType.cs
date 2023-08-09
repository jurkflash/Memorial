using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.GenderType
{
    public class GenderType : IGenderType
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenderType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Core.Domain.GenderType> GetAll()
        {
            return _unitOfWork.GenderTypes.GetAllActive();
        }
    }
}