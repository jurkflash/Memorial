﻿using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Lib.Urn;

namespace Memorial.Lib.Invoice
{
    public class Urn : Invoice, IUrn
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;
        protected ITransaction _transaction;

        public Urn(IUnitOfWork unitOfWork, INumber number, ITransaction transaction) : base(unitOfWork)
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

            _invoice.UrnTransactionAF = AF;

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
            return GetInvoicesByUrnAF(AF);
        }

        public IEnumerable<Core.Dtos.InvoiceDto> GetInvoiceDtos(string AF)
        {
            return GetInvoiceDtosByUrnAF(AF);
        }
    }
}