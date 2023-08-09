using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.ReligionType
{
    public class ReligionType : IReligionType
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReligionType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Core.Domain.ReligionType> GetAll()
        {
            return _unitOfWork.ReligionTypes.GetAllActive();
        }
    }
}