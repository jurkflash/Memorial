using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.SubProductService
{
    public class SubProductService : ISubProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.SubProductService Get(int id)
        {
            return _unitOfWork.SubProductServices.Get(id);
        }

        public IEnumerable<Core.Domain.SubProductService> GetAll()
        {
            return _unitOfWork.SubProductServices.GetAll();
        }

        public IEnumerable<Core.Domain.SubProductService> GetByProduct(int productId)
        {
            return _unitOfWork.SubProductServices.GetSubProductServicesByProductId(productId);
        }

        public IEnumerable<Core.Domain.SubProductService> GetByProductIdAndOtherId(int productId, int otherId)
        {
            return _unitOfWork.SubProductServices.GetSubProductServicesByProductIdAndOtherId(productId, otherId);
        }
    }
}