using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.PaymentMethod
{
    public class PaymentMethod : IPaymentMethod
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentMethod(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.PaymentMethod Get(int id)
        {
            return _unitOfWork.PaymentMethods.Get(id);
        }

        public IEnumerable<Core.Domain.PaymentMethod> GetAll()
        {
            return _unitOfWork.PaymentMethods.GetAll();
        }
    }
}