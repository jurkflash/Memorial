using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.NationalityType
{
    public class NationalityType : INationalityType
    {
        private readonly IUnitOfWork _unitOfWork;

        public NationalityType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Core.Domain.NationalityType> GetAll()
        {
            return _unitOfWork.NationalityTypes.GetAllActive();
        }
    }
}