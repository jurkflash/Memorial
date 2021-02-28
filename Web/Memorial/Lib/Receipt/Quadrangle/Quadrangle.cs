using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.Lib.Quadrangle;
using AutoMapper;

namespace Memorial.Lib.Receipt
{
    public class Quadrangle : Receipt, IQuadrangle
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;
        protected ITransaction _transaction;

        public Quadrangle(
            IUnitOfWork unitOfWork, 
            INumber number, 
            IPaymentMethod paymentMethod
            ) : base(
                unitOfWork, 
                paymentMethod)
        {
            _unitOfWork = unitOfWork;
            _number = number;
        }

        public void SetTransaction(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public void SetInvoice(Invoice.IQuadrangle invoice)
        {
            _invoice = invoice;
        }

        public IEnumerable<Core.Domain.Receipt> GetNonOrderReceipts()
        {
            return _unitOfWork.Receipts.GetByNonOrderActiveQuadrangleAF(_transaction.GetAF());
        }

        public IEnumerable<ReceiptDto> GetNonOrderReceiptDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(GetNonOrderReceipts());
        }

        override
        public void NewNumber()
        {
            _reNumber = _number.GetNewIV(_transaction.GetItemId(), System.DateTime.Now.Year);
        }

        public float GetTotalIssuedNonOrderReceiptAmount()
        {
            return GetNonOrderReceipts().Sum(r => r.Amount);
        }

        public void SetTotalIssuedNonOrderReceiptAmount()
        {
            _nonOrderTotalIssuedReceiptsAmount = GetTotalIssuedNonOrderReceiptAmount();
        }

        public void SetNonOrderAmount()
        {
            _nonOrderAmount = _transaction.GetAmount();
        }

        public float GetNonOrderAmount()
        {
            return _nonOrderAmount;
        }

        public bool NonOrderCreate(float amount, string remark, byte paymentMethodId, string paymentRemark)
        {
            SetNew();

            _receipt.QuadrangleTransactionAF = _transaction.GetAF();

            Create(amount, remark, paymentMethodId, paymentRemark);

            _unitOfWork.Complete();

            return true;
        }

        public bool OrderCreate(string IV, float amount, string remark, byte paymentMethodId, string paymentRemark)
        {
            SetNew();

            _receipt.QuadrangleTransactionAF = _transaction.GetAF();

            _receipt.InvoiceIV = IV;

            Create(amount, remark, paymentMethodId, paymentRemark);

            _invoice.SetInvoice(IV);
            _invoice.SetHasReceipt(true);
            if(GetTotalIssuedOrderReceiptAmount() + amount == _invoice.GetAmount())
            {
                _invoice.SetIsPaid(true);
            }

            _unitOfWork.Complete();

            return true;
        }

    }
}