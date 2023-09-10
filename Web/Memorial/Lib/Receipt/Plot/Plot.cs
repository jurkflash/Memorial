using Memorial.Core;
using System.Collections.Generic;
using Memorial.Lib.Cemetery;

namespace Memorial.Lib.Receipt
{
    public class Plot : Receipt, IPlot
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentMethod _paymentMethod;
        protected INumber _number;

        public Plot(
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
            var transaction = _unitOfWork.CemeteryTransactions.GetByAF(receipt.CemeteryTransactionAF);
            total = transaction.Price +
                (transaction.Maintenance == null ? 0 : (float)transaction.Maintenance) +
                (transaction.Wall == null ? 0 : (float)transaction.Wall) +
                (transaction.Dig == null ? 0 : (float)transaction.Dig) +
                (transaction.Brick == null ? 0 : (float)transaction.Brick);

            var receiptInDb = _unitOfWork.Receipts.GetByRE(RE);
            if (_paymentMethod.Get(receipt.PaymentMethodId).RequireRemark && receipt.PaymentRemark == "")
                return false;

            if (transaction.CemeteryItem.isOrder == true)
            {
                var invoice = _unitOfWork.Invoices.GetByIV(receipt.InvoiceIV);
                if (invoice.Amount < GetTotalIssuedReceiptAmountByIV(receipt.InvoiceIV) - receiptInDb.Amount + receipt.Amount)
                    return false;
            }

            if (receiptInDb.Amount != receipt.Amount && total < (GetTotalIssuedReceiptAmountByAF(receipt.CemeteryTransactionAF) - receiptInDb.Amount + receipt.Amount))
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
            var transaction = _unitOfWork.CemeteryTransactions.GetByAF(receipt.CemeteryTransactionAF);
            var receiptsTotalAmount = _unitOfWork.Receipts.GetTotalAmountByCemeteryAF(receipt.CemeteryTransactionAF);
            total = transaction.Price +
                (transaction.Maintenance == null ? 0 : (float)transaction.Maintenance) +
                (transaction.Wall == null ? 0 : (float)transaction.Wall) +
                (transaction.Dig == null ? 0 : (float)transaction.Dig) +
                (transaction.Brick == null ? 0 : (float)transaction.Brick);

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
            return _unitOfWork.Receipts.GetByCemeteryAF(AF);
        }

        override
        public float GetTotalIssuedReceiptAmountByAF(string AF)
        {
            return _unitOfWork.Receipts.GetTotalAmountByCemeteryAF(AF);
        }
    }
}