using System.Collections.Generic;

namespace Memorial.Lib
{
    public interface IPaymentMethod
    {
        Core.Domain.PaymentMethod Get(int id);
        IEnumerable<Core.Domain.PaymentMethod> GetAll();
    }
}