using Memorial.Core;
using AutoMapper;
using System.Linq;

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

        public Core.Domain.Invoice GetByIV(string IV)
        {
            return _unitOfWork.Invoices.GetByIV(IV);
        }

        protected bool Change(Core.Domain.Invoice invoice)
        {
            var invoiceInDb = _unitOfWork.Invoices.GetByIV(invoice.IV);
            invoiceInDb.hasReceipt = invoice.hasReceipt;
            invoiceInDb.isPaid = invoice.isPaid;
            invoiceInDb.AllowDeposit = invoice.AllowDeposit;
            invoiceInDb.Amount = invoice.Amount;
            invoiceInDb.Remark = invoice.Remark;
            _unitOfWork.Complete();
            return true;
        }

        virtual
        public bool Remove(Core.Domain.Invoice invoice)
        {
            var invoiceInDb = _unitOfWork.Invoices.GetByIV(invoice.IV);
            if(invoiceInDb != null && !_unitOfWork.Receipts.GetByIV(invoice.IV).Any())
            {
                _unitOfWork.Invoices.Remove(invoiceInDb);
                _unitOfWork.Complete();
                return true;
            }
            return false;
        }

        public float GetUnpaidAmount(Core.Domain.Invoice invoice)
        {
            var totalPaid = _unitOfWork.Receipts.GetByIV(invoice.IV).Select(r => r.Amount).DefaultIfEmpty(0).Sum();
            return invoice.Amount - totalPaid;
        }


        public void SetInvoice(string IV)
        {
            _invoice = _unitOfWork.Invoices.GetByIV(IV);
        }

        public Core.Domain.Invoice GetInvoice()
        {
            return _invoice;
        }

        public Core.Domain.Invoice GetInvoice(string IV)
        {
            return _unitOfWork.Invoices.GetByIV(IV);
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
        public string GenerateIVNumber(int itemId);

        protected bool Add(Core.Domain.Invoice invoice)
        {
            _unitOfWork.Invoices.Add(invoice);
            _unitOfWork.Complete();

            return true;
        }

        protected bool UpdateInvoice(InvoiceDto invoiceDto)
        {
            var invoiceInDb = GetInvoice(invoiceDto.IV);

            Mapper.Map(invoiceDto, invoiceInDb);

            return true;
        }

        protected bool DeleteInvoice()
        {
            _unitOfWork.Invoices.Remove(_invoice);

            return true;
        }

    }
}