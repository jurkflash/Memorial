using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Lib.Miscellaneous;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Invoice
{
    public class Miscellaneous : Invoice, IMiscellaneous
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;

        public Miscellaneous(IUnitOfWork unitOfWork, INumber number) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _number = number;
        }

        public IEnumerable<Core.Domain.Invoice> GetInvoicesByAF(string AF)
        {
            return _unitOfWork.Invoices.GetByActiveMiscellaneousAF(AF);
        }

        public IEnumerable<Core.Dtos.InvoiceDto> GetInvoiceDtosByAF(string AF)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(GetInvoicesByAF(AF));
        }

        public bool HasInvoiceByAF(string AF)
        {
            return _unitOfWork.Invoices.GetByActiveMiscellaneousAF(AF).Any();
        }

        override
        public string GetAF()
        {
            return _invoice.MiscellaneousTransactionAF;
        }

        override
        public void NewNumber(int itemId)
        {
            _ivNumber = _number.GetNewIV(itemId, System.DateTime.Now.Year);
        }

        public bool Create(int itemId, InvoiceDto invoiceDto)
        {
            NewNumber(itemId);

            CreateNewInvoice(invoiceDto);

            return true;
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
            var invoices = GetInvoicesByAF(AF);
            foreach (var invoice in invoices)
            {
                _unitOfWork.Invoices.Remove(invoice);
            }

            return true;
        }
    }
}