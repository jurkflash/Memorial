using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.MaritalType
{
    public class MaritalType : IMaritalType
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaritalType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Core.Domain.MaritalType> GetAll()
        {
            return _unitOfWork.MaritalTypes.GetAllActive();
        }
    }
}