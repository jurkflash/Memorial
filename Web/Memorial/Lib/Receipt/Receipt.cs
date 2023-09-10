using System.Collections.Generic;
using System.Linq;
using Memorial.Core;

namespace Memorial.Lib.Receipt
{
    public abstract class Receipt : IReceipt
    {
        private readonly IUnitOfWork _unitOfWork;
        protected Core.Domain.Receipt _receipt;
        protected string _reNumber;
        protected float _nonOrderAmount;
        protected float _nonOrderTotalIssuedReceiptsAmount;

        public Receipt(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Receipt GetByRE(string RE)
        {
            return _unitOfWork.Receipts.GetByRE(RE);
        }

        virtual
        public bool Remove(Core.Domain.Receipt receipt)
        {
            var receiptInDb = _unitOfWork.Receipts.GetByRE(receipt.RE);

            if (receiptInDb.InvoiceIV != null)
            {
                var receipts = _unitOfWork.Receipts.GetByIV(receiptInDb.InvoiceIV).ToList();
                var invoiceInDb = _unitOfWork.Invoices.GetByIV(receiptInDb.InvoiceIV);
                invoiceInDb.hasReceipt = receipts.Any();
                invoiceInDb.isPaid = receipts.Select(r => r.Amount).DefaultIfEmpty(0).Sum() == invoiceInDb.Amount;

                _unitOfWork.Receipts.Remove(receiptInDb);
                _unitOfWork.Complete();
            }
            return true;
        }

        protected bool Change(Core.Domain.Receipt receipt)
        {
            var receiptInDb = _unitOfWork.Receipts.GetByRE(receipt.RE);
            receiptInDb.Remark = receipt.Remark;
            receiptInDb.PaymentRemark = receipt.PaymentRemark;
            receiptInDb.PaymentMethodId = receipt.PaymentMethodId;
            receiptInDb.Amount = receipt.Amount;
            _unitOfWork.Complete();
            return true;
        }       

        protected bool Add(Core.Domain.Receipt receipt)
        {
            _unitOfWork.Receipts.Add(receipt);
            _unitOfWork.Complete();

            return true;
        }

        public void SetReceipt(string RE)
        {
            _receipt = _unitOfWork.Receipts.GetByRE(RE);
        }

        public IEnumerable<Core.Domain.Receipt> GetByIV(string IV)
        {
            return _unitOfWork.Receipts.GetByIV(IV);
        }

        public string GetInvoiceIV()
        {
            return _receipt.InvoiceIV;
        }

        public bool isOrderReceipt()
        {
            return _receipt.InvoiceIV == null ? false : true;
        }

        public float GetTotalIssuedReceiptAmountByIV(string IV)
        {
            return GetByIV(IV).Sum(r => r.Amount);
        }

        public bool DeleteByIV(string IV)
        {
            var receipts = GetByIV(IV);
            foreach(var receipt in receipts)
            {
                _unitOfWork.Receipts.Remove(receipt);
            }

            //_invoice.SetInvoice(_receipt.InvoiceIV);
            //_invoice.SetIsPaid(false);
            //_invoice.SetHasReceipt(false);

            return true;
        }


        abstract
        public IEnumerable<Core.Domain.Receipt> GetByAF(string AF);

        abstract
        public string GenerateRENumber(int itemId);

        abstract
        public float GetTotalIssuedReceiptAmountByAF(string AF);
    }
}