using Memorial.Core;
using System.Collections.Generic;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Lib.Receipt
{
    public class AncestralTablet : Receipt, IAncestralTablet
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentMethod _paymentMethod;
        protected INumber _number;

        public AncestralTablet(
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
            float total = 0;
            var transaction = _unitOfWork.AncestralTabletTransactions.GetByAF(receipt.AncestralTabletTransactionAF);
            total = transaction.Price + (transaction.Maintenance == null ? 0 : (float)transaction.Maintenance);

            var receiptInDb = _unitOfWork.Receipts.GetByRE(RE);
            if (_paymentMethod.Get(receipt.PaymentMethodId).RequireRemark && receipt.PaymentRemark == "")
                return false;

            if (transaction.AncestralTabletItem.isOrder == true)
            {
                var invoice = _unitOfWork.Invoices.GetByIV(receipt.InvoiceIV);
                if (invoice.Amount < GetTotalIssuedReceiptAmountByIV(receipt.InvoiceIV) - receiptInDb.Amount + receipt.Amount)
                    return false;
            }

            if (receiptInDb.Amount != receipt.Amount && total < (GetTotalIssuedReceiptAmountByAF(receipt.AncestralTabletTransactionAF) - receiptInDb.Amount + receipt.Amount))
            {
                return false;
            }

            return Change(receipt);
        }

        override
        public string GenerateRENumber(int itemId)
        {
            return _number.GetNewRE(itemId, System.DateTime.Now.Year);
        }

        public bool Add(int itemId, Core.Domain.Receipt receipt)
        {
            float total = 0;
            Core.Domain.Invoice invoiceInDb = null;
            var transaction = _unitOfWork.AncestralTabletTransactions.GetByAF(receipt.AncestralTabletTransactionAF);
            var receiptsTotalAmount = _unitOfWork.Receipts.GetTotalAmountBySpaceAF(receipt.AncestralTabletTransactionAF);
            total = transaction.Price + (transaction.Maintenance == null ? 0 : (float)transaction.Maintenance);

            if (receipt.InvoiceIV != null)
            {
                invoiceInDb = _unitOfWork.Invoices.GetByIV(receipt.InvoiceIV);
                total = invoiceInDb.Amount;
            }

            if (total < receipt.Amount || receiptsTotalAmount < receipt.Amount)
                return false;

            if (receipt.InvoiceIV != null)
            {
                invoiceInDb.hasReceipt = true;
                var amount = GetTotalIssuedReceiptAmountByIV(receipt.InvoiceIV);
                if (invoiceInDb.Amount - amount == receipt.Amount)
                {
                    invoiceInDb.isPaid = true;
                }
            }

            var re = GenerateRENumber(itemId);

            receipt.RE = re;
            var status = Add(receipt);

            return true;
        }

        override
        public IEnumerable<Core.Domain.Receipt> GetByAF(string AF)
        {
            return _unitOfWork.Receipts.GetByAncestralTabletAF(AF);
        }

        override
        public float GetTotalIssuedReceiptAmountByAF(string AF)
        {
            return _unitOfWork.Receipts.GetTotalAmountByAncestralTabletAF(AF);
        }
    }
}