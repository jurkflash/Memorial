using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    public class PaymentMethodsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentMethodsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetPaymentMethod(byte id)
        {
            var paymentMethodInDb = _unitOfWork.PaymentMethods.Get(id);

            if (paymentMethodInDb == null)
                return NotFound();

            return Ok(Mapper.Map<PaymentMethod,PaymentMethodDto>(paymentMethodInDb));
        }
    }
}
