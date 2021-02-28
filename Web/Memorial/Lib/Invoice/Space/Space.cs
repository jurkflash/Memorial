using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Lib.Space;

namespace Memorial.Lib.Invoice
{
    public class Space : Invoice, IQuadrangle
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;
        protected ITransaction _transaction;

        public Space(IUnitOfWork unitOfWork, INumber number, ITransaction transaction) : base(unitOfWork)
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

            _invoice.SpaceTransactionAF = AF;

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
            return GetInvoicesBySpaceAF(AF);
        }

        public IEnumerable<Core.Dtos.InvoiceDto> GetInvoiceDtos(string AF)
        {
            return GetInvoiceDtosBySpaceAF(AF);
        }
    }
}