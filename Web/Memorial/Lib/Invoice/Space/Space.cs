using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Lib.Space;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Invoice
{
    public class Space : Invoice, ISpace
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;

        public Space(IUnitOfWork unitOfWork, INumber number) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _number = number;
        }

        public bool Change(string IV, Core.Domain.Invoice invoice)
        {
            var transaction = _unitOfWork.SpaceTransactions.GetByAF(invoice.SpaceTransactionAF);
            if (transaction.Amount + transaction.OtherCharges < invoice.Amount)
                return false;

            var totalReceiptAmount = _unitOfWork.Receipts.GetTotalAmountBySpaceAF(invoice.SpaceTransactionAF);
            if (totalReceiptAmount < transaction.Amount)
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




        public IEnumerable<Core.Domain.Invoice> GetByAF(string AF)
        {
            return _unitOfWork.Invoices.GetByActiveSpaceAF(AF);
        }

        public bool HasInvoiceByAF(string AF)
        {
            return _unitOfWork.Invoices.GetByActiveSpaceAF(AF).Any();
        }

        override
        public string GetAF()
        {
            return _invoice.SpaceTransactionAF;
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

        public bool Update(InvoiceDto invoiceDto)
        {
            UpdateInvoice(invoiceDto);

            return true;
        }

        public bool Delete()
        {
            DeleteInvoice();

            return true;
        }

        public bool DeleteByApplication(string AF)
        {
            var invoices = GetByAF(AF);
            foreach (var invoice in invoices)
            {
                _unitOfWork.Invoices.Remove(invoice);
            }

            return true;
        }
    }
}