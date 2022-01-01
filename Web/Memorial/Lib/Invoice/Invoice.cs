using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;
using Memorial.Lib.Columbarium;
using AutoMapper;

namespace Memorial.Lib.Invoice
{
    public abstract class Invoice : IInvoice
    {
        private readonly IUnitOfWork _unitOfWork;
        protected Core.Domain.Invoice _invoice;
        protected string _ivNumber;

        public Invoice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetInvoice(string IV)
        {
            _invoice = _unitOfWork.Invoices.GetByActiveIV(IV);
        }

        public void SetInvoice(InvoiceDto invoiceDto)
        {
            _invoice = Mapper.Map<InvoiceDto, Core.Domain.Invoice>(invoiceDto);
        }
        public Core.Domain.Invoice GetInvoice()
        {
            return _invoice;
        }

        public InvoiceDto GetInvoiceDto()
        {
            return Mapper.Map<Core.Domain.Invoice, InvoiceDto>(_invoice);
        }

        public Core.Domain.Invoice GetInvoice(string IV)
        {
            return _unitOfWork.Invoices.GetByActiveIV(IV);
        }

        public InvoiceDto GetInvoiceDto(string IV)
        {
            return Mapper.Map<Core.Domain.Invoice, InvoiceDto>(GetInvoice(IV));
        }

        public string GetIV()
        {
            return _invoice.IV;
        }

        public float GetAmount()
        {
            return _invoice.Amount;
        }

        public void SetAmount(float amount)
        {
            _invoice.Amount = amount;
        }

        public bool IsPaid()
        {
            return _invoice.isPaid;
        }

        public void SetIsPaid(bool paid)
        {
            _invoice.isPaid = paid;
        }

        public string GetRemark()
        {
            return _invoice.Remark;
        }

        public void SetRemark(string remark)
        {
            _invoice.Remark = remark;
        }

        public bool HasReceipt()
        {
            return _invoice.hasReceipt;
        }

        public void SetHasReceipt(bool hasReceipt)
        {
            _invoice.hasReceipt = hasReceipt;
        }

        abstract
        public string GetAF();

        abstract
        public void NewNumber(int itemId);

        protected bool CreateNewInvoice(InvoiceDto invoiceDto)
        {
            if (string.IsNullOrEmpty(_ivNumber))
                return false;

            _invoice = new Core.Domain.Invoice();

            Mapper.Map(invoiceDto, _invoice);

            _invoice.IV = _ivNumber;
            _invoice.hasReceipt = false;
            _invoice.CreatedDate = System.DateTime.Now;

            _unitOfWork.Invoices.Add(_invoice);

            return true;
        }

        protected bool UpdateInvoice(InvoiceDto invoiceDto)
        {
            var invoiceInDb = GetInvoice(invoiceDto.IV);

            Mapper.Map(invoiceDto, invoiceInDb);

            invoiceInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteInvoice()
        {
            _invoice.DeleteDate = System.DateTime.Now;

            return true;
        }

    }
}