using Memorial.Core;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;
using AutoMapper;

namespace Memorial.Lib.Receipt
{
    public class Space : Receipt, ISpace
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentMethod _paymentMethod;
        protected INumber _number;

        public Space(
            IUnitOfWork unitOfWork,
            IPaymentMethod paymentMethod,
            INumber number
            ) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _paymentMethod = paymentMethod;
            _number = number;
        }

        public bool Change(string RE, Core.Domain.Receipt receipt)
        {
            var transaction = _unitOfWork.SpaceTransactions.GetByAF(receipt.SpaceTransactionAF);

            var receiptInDb = _unitOfWork.Receipts.GetByRE(RE);
            if (_paymentMethod.Get(receipt.PaymentMethodId).RequireRemark && receipt.PaymentRemark == "")
                return false;

            if(transaction.SpaceItem.isOrder == true)
            {
                var invoice = _unitOfWork.Invoices.GetByIV(receipt.InvoiceIV);
                if (invoice.Amount < GetTotalIssuedReceiptAmountByIV(receipt.InvoiceIV) - receiptInDb.Amount + receipt.Amount)
                    return false;
            }

            if (transaction.Amount < GetTotalIssuedReceiptAmount(receipt.SpaceTransactionAF))
                return false;

            return Change(receipt);
        }

        override
        public string GenerateRENumber(int itemId)
        {
            return _number.GetNewRE(itemId, System.DateTime.Now.Year);
        }

        public bool Add(int itemId, Core.Domain.Receipt receipt)
        {
            var re = GenerateRENumber(itemId);

            receipt.RE = re;
            var status = Add(receipt);
            
            if(receipt.InvoiceIV != null)
            {
                var invoiceInDb = _unitOfWork.Invoices.GetByIV(receipt.InvoiceIV);
                invoiceInDb.hasReceipt = true;
                var amount = GetTotalIssuedReceiptAmountByIV(receipt.InvoiceIV);
                if(amount == invoiceInDb.Amount)
                {
                    invoiceInDb.isPaid = true;
                }
                _unitOfWork.Complete();
            }

            return true;
        }

        public IEnumerable<Core.Domain.Receipt> GetByAF(string AF)
        {
            return _unitOfWork.Receipts.GetBySpaceAF(AF);
        }





        public IEnumerable<ReceiptDto> GetNonOrderReceiptDtos(string AF)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(GetNonOrderReceipts(AF));
        }

        public string GetApplicationAF()
        {
            return _receipt.SpaceTransactionAF;
        }

        override
        public void NewNumber(int itemId)
        {
            _reNumber = _number.GetNewRE(itemId, System.DateTime.Now.Year);
        }

        public float GetTotalIssuedReceiptAmount(string AF)
        {
            return _unitOfWork.Receipts.GetTotalAmountBySpaceAF(AF);
        }

        public bool Create(int itemId, ReceiptDto receiptDto)
        {
            NewNumber(itemId);

            CreateNewReceipt(receiptDto);

            return true;
        }

        public bool Update(ReceiptDto receiptDto)
        {
            UpdateReceipt(receiptDto);

            return true;
        }

        public bool Delete()
        {
            DeleteReceipt();

            return true;
        }

        public bool DeleteNonOrderReceiptsByApplicationAF(string AF)
        {
            var receipts = GetNonOrderReceipts(AF);
            foreach (var receipt in receipts)
            {
                _unitOfWork.Receipts.Remove(receipt);
            }

            return true;
        }

    }
}