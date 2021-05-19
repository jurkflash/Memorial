using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;
using Memorial.Lib.Columbarium;
using AutoMapper;

namespace Memorial.Lib.PaymentMethod
{
    public class PaymentMethod : IPaymentMethod
    {
        private readonly IUnitOfWork _unitOfWork;
        protected Core.Domain.PaymentMethod _paymentMethod;

        public PaymentMethod(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetPaymentMethod(int paymentMethodId)
        {
            _paymentMethod = _unitOfWork.PaymentMethods.Get(paymentMethodId);
        }

        public Core.Domain.PaymentMethod GetPaymentMethod()
        {
            return _paymentMethod;
        }

        public Core.Domain.PaymentMethod GetPaymentMethod(int paymentMethodId)
        {
            return _unitOfWork.PaymentMethods.Get(paymentMethodId);
        }

        public string GetName()
        {
            return _paymentMethod.Name;
        }

        public bool GetRequireRemark()
        {
            return _paymentMethod.RequireRemark;
        }

        public IEnumerable<Core.Domain.PaymentMethod> GetPaymentMethods()
        {
            return _unitOfWork.PaymentMethods.GetAll(); 
        }

        public IEnumerable<PaymentMethodDto> GetPaymentMethodDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.PaymentMethod>, IEnumerable<PaymentMethodDto>>(GetPaymentMethods());
        }


    }
}