using Memorial.Core;
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
            invoiceInDb.isPaid = invoice.isPaid;
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

        abstract
        public string GenerateIVNumber(int itemId);

        protected bool Add(Core.Domain.Invoice invoice)
        {
            _unitOfWork.Invoices.Add(invoice);
            _unitOfWork.Complete();

            return true;
        }
    }
}