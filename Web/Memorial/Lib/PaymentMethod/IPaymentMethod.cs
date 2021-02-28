using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IPaymentMethod
    {
        void SetPaymentMethod(int paymentMethodId);

        Core.Domain.PaymentMethod GetPaymentMethod();

        Core.Domain.PaymentMethod GetPaymentMethod(int paymentMethodId);

        string GetName();

        bool GetRequireRemark();

        IEnumerable<Core.Domain.PaymentMethod> GetPaymentMethods();

        IEnumerable<PaymentMethodDto> GetPaymentMethodDtos();
    }
}