﻿using Memorial.Core;
using System.Collections.Generic;
using Memorial.Lib.Space;

namespace Memorial.Lib.Invoice
{
    public class Space : Invoice, ISpace
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransaction _transaction;
        protected INumber _number;

        public Space(IUnitOfWork unitOfWork, INumber number, ITransaction transaction) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _number = number;
            _transaction = transaction;
        }

        public IEnumerable<Core.Domain.Invoice> GetByAF(string AF)
        {
            return _unitOfWork.Invoices.GetByActiveSpaceAF(AF);
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
            var transaction = _unitOfWork.SpaceTransactions.GetByAF(invoice.SpaceTransactionAF);
            
            var total = _transaction.GetTotalAmount(transaction);
            if (total < invoice.Amount)
                return false;

            var totalReceiptAmount = _unitOfWork.Receipts.GetTotalAmountBySpaceAF(invoice.SpaceTransactionAF);
            if (totalReceiptAmount > invoice.Amount)
                return false;

            var totalPaidInvoiceAmount = _unitOfWork.Receipts.GetTotalAmountByIV(IV);
            if (totalPaidInvoiceAmount > invoice.Amount)
                return false;

            if (invoice.Amount == total - totalReceiptAmount)
                invoice.isPaid = true;
            else
                invoice.isPaid = false;

            invoice.IV = IV;
            Change(invoice);
            return true;
        }
    }
}