using Memorial.Core;
using System.Collections.Generic;
using Memorial.Lib.Urn;

namespace Memorial.Lib.Invoice
{
    public class Urn : Invoice, IUrn
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;

        public Urn(IUnitOfWork unitOfWork, INumber number) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _number = number;
        }

        public IEnumerable<Core.Domain.Invoice> GetByAF(string AF)
        {
            return _unitOfWork.Invoices.GetByActiveUrnAF(AF);
        }

        override
        public string GenerateIVNumber(int itemId)
        {
            return _number.GetNewIV(itemId, System.DateTime.Now.Year);
        }

        public bool Add(int itemId, Core.Domain.Invoice invoice)
        {
            var iv = GenerateIVNumber(itemId);

            invoice.IV = iv;
            return Add(invoice);
        }

        public bool Change(string IV, Core.Domain.Invoice invoice)
        {
            var transaction = _unitOfWork.UrnTransactions.GetByAF(invoice.UrnTransactionAF);
            var total = transaction.Price;
            if (total < invoice.Amount)
                return false;

            var totalReceiptAmount = _unitOfWork.Receipts.GetTotalAmountByUrnAF(invoice.UrnTransactionAF);
            if (totalReceiptAmount < total)
                return false;

            var invoiceInDB = _unitOfWork.Invoices.GetByIV(IV);
            if (invoiceInDB.Amount < invoice.Amount)
                return false;

            if (invoice.Amount == totalReceiptAmount)
                invoice.isPaid = true;
            else
                invoice.isPaid = false;

            Change(invoice);
            _unitOfWork.Complete();
            return true;
        }
    }
}