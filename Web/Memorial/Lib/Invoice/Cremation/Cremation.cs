using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Lib.Cremation;

namespace Memorial.Lib.Invoice
{
    public class Cremation : Invoice, ICremation
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;
        protected ITransaction _transaction;

        public Cremation(IUnitOfWork unitOfWork, INumber number, ITransaction transaction) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _number = number;
            _transaction = transaction;
        }

        override
        protected void NewNumber()
        {
            _ivNumber = _number.GetNewIV(_transaction.GetItemId(), System.DateTime.Now.Year);
        }

        public bool Create(string AF, float amount, string remark)
        {
            SetNew();

            NewNumber();

            _invoice.CremationTransactionAF = AF;

            CreateNewInvoice(amount, remark);

            _unitOfWork.Complete();

            return true;
        }

        public bool Update(float amount, string remark)
        {
            _invoice.Remark = remark;
            _invoice.Amount = amount;
            _unitOfWork.Complete();
            return true;
        }

        public IEnumerable<Core.Domain.Invoice> GetInvoices(string AF)
        {
            return GetInvoicesByCremationAF(AF);
        }

        public IEnumerable<Core.Dtos.InvoiceDto> GetInvoiceDtos(string AF)
        {
            return GetInvoiceDtosByCremationAF(AF);
        }
    }
}