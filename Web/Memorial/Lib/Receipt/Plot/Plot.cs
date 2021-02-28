using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.Lib.Plot;
using AutoMapper;

namespace Memorial.Lib.Receipt
{
    public class Plot : Receipt, IPlot
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;
        protected ITransaction _transaction;

        public Plot(IUnitOfWork unitOfWork, INumber number, ITransaction transaction, IInvoice invoice, IPaymentMethod paymentMethod) : base(unitOfWork, invoice, paymentMethod)
        {
            _unitOfWork = unitOfWork;
            _number = number;
            _transaction = transaction;
        }

        protected IEnumerable<Core.Domain.Receipt> GetNonOrderReceipts(string AF)
        {
            return _unitOfWork.Receipts.GetByNonOrderActivePlotAF(AF);
        }

        protected IEnumerable<ReceiptDto> GetNonOrderReceiptDtos(string AF)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(GetNonOrderReceipts(AF));
        }

        override
        protected void NewNumber()
        {
            _reNumber = _number.GetNewIV(_transaction.GetItemId(), System.DateTime.Now.Year);
        }

        public void SetTotalIssuedNonOrderReceiptAmmount(string AF)
        {
            _nonOrderTotalIssuedReceiptsAmount = GetNonOrderReceipts(AF).Sum(r => r.Amount);
        }

        public float GetTotalIssuedNonOrderReceiptAmmount()
        {
            return _nonOrderTotalIssuedReceiptsAmount;
        }

        public void SetNonOrderAmmount(string AF)
        {
            _transaction.SetTransaction(AF);
            _nonOrderAmount = _transaction.GetAmount();
        }

        public float GetNonOrderAmmount()
        {
            return _nonOrderAmount;
        }

        public bool Create(string AF, string IV, float amount, string remark, byte paymentMethodId, string paymentRemark)
        {
            SetNew();

            NewNumber();

            _receipt.PlotTransactionAF = AF;

            CreateNewReceipt(IV, amount, remark, paymentMethodId, paymentRemark);

            _unitOfWork.Complete();

            return true;
        }

    }
}