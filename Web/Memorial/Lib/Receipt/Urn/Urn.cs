using Memorial.Core;
using System.Collections.Generic;
using Memorial.Lib.Urn;

namespace Memorial.Lib.Receipt
{
    public class Urn : Receipt, IUrn
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentMethod _paymentMethod;
        private readonly ITransaction _transaction;
        private readonly IItem _item;
        protected INumber _number;

        public Urn(
            IUnitOfWork unitOfWork,
            IPaymentMethod paymentMethod,
            ITransaction transaction,
            IItem item,
            INumber number
            ) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _paymentMethod = paymentMethod;
            _transaction = transaction;
            _item = item;
            _number = number;
        }

        public bool Change(string RE, Core.Domain.Receipt receipt)
        {
            if (_paymentMethod.Get(receipt.PaymentMethodId).RequireRemark && receipt.PaymentRemark == "")
                return false;

            float total = 0;
            var transaction = _unitOfWork.UrnTransactions.GetByAF(receipt.UrnTransactionAF);
            total = _transaction.GetTotalAmount(transaction);

            var receiptInDb = _unitOfWork.Receipts.GetByRE(RE);
            var item = _item.GetById(transaction.UrnItemId);
            if (_item.IsOrder(item))
            {
                var invoice = _unitOfWork.Invoices.GetByIV(receipt.InvoiceIV);
                var receiptsTotalAmount = GetTotalIssuedReceiptAmountByIV(receipt.InvoiceIV);
                if (invoice.Amount < receiptsTotalAmount - receiptInDb.Amount + receipt.Amount)
                    return false;

                if (invoice.Amount > receiptsTotalAmount - receiptInDb.Amount + receipt.Amount)
                    invoice.isPaid = false;
                else
                    invoice.isPaid = true;
            }

            if (receiptInDb.Amount != receipt.Amount && total < (GetTotalIssuedReceiptAmountByAF(receipt.SpaceTransactionAF) - receiptInDb.Amount + receipt.Amount))
            {
                return false;
            }

            receipt.RE = RE;
            return Change(receipt);
        }

        override
        public string GenerateRENumber(int itemId)
        {
            return _number.GetNewRE(itemId, System.DateTime.Now.Year);
        }

        public bool Add(int itemId, Core.Domain.Receipt receipt)
        {
            if (_paymentMethod.Get(receipt.PaymentMethodId).RequireRemark && receipt.PaymentRemark == "")
                return false;

            float total = 0;
            Core.Domain.Invoice invoiceInDb = null;
            var transaction = _unitOfWork.UrnTransactions.GetByAF(receipt.UrnTransactionAF);
            var receiptsTotalAmount = _unitOfWork.Receipts.GetTotalAmountByUrnAF(receipt.UrnTransactionAF);
            total = _transaction.GetTotalAmount(transaction);

            var item = _item.GetById(itemId);
            if (_item.IsOrder(item))
            {
                invoiceInDb = _unitOfWork.Invoices.GetByIV(receipt.InvoiceIV);
                total = invoiceInDb.Amount;

                var amount = GetTotalIssuedReceiptAmountByIV(receipt.InvoiceIV);
                if (total - amount < receipt.Amount)
                    return false;

                if (total - amount == receipt.Amount)
                    invoiceInDb.isPaid = true;
                invoiceInDb.hasReceipt = true;
            }
            else
            {
                if (total - receiptsTotalAmount < receipt.Amount)
                    return false;
            }

            var re = GenerateRENumber(itemId);

            receipt.RE = re;
            var status = Add(receipt);

            return true;
        }

        override
        public IEnumerable<Core.Domain.Receipt> GetByAF(string AF)
        {
            return _unitOfWork.Receipts.GetByUrnAF(AF);
        }

        override
        public float GetTotalIssuedReceiptAmountByAF(string AF)
        {
            return _unitOfWork.Receipts.GetTotalAmountByUrnAF(AF);
        }
    }
}