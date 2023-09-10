using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.Columbarium
{
    public class NicheType : INicheType
    {
        private readonly IUnitOfWork _unitOfWork;

        public NicheType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Core.Domain.NicheType> GetAll()
        {
            return _unitOfWork.NicheTypes.GetAll();
        }
    }
}