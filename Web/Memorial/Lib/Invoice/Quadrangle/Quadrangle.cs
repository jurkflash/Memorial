using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Lib.Quadrangle;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Invoice
{
    public class Quadrangle : Invoice, IQuadrangle
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;

        public Quadrangle(IUnitOfWork unitOfWork, INumber number) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _number = number;
        }

        public IEnumerable<Core.Domain.Invoice> GetInvoicesByAF(string AF)
        {
            return _unitOfWork.Invoices.GetByActiveQuadrangleAF(AF);
        }

        public IEnumerable<Core.Dtos.InvoiceDto> GetInvoiceDtosByAF(string AF)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(GetInvoicesByAF(AF));
        }

        override
        public string GetAF()
        {
            return _invoice.QuadrangleTransactionAF;
        }

        override
        public void NewNumber(int itemId)
        {
            _ivNumber = _number.GetNewIV(itemId, System.DateTime.Now.Year);
        }

        public bool Create(int itemId, string AF, float amount, string remark)
        {
            SetNew();

            NewNumber(itemId);

            _invoice.QuadrangleTransactionAF = AF;

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

        public bool DeleteByApplication(string AF)
        {
            var invoices = GetInvoicesByAF(AF);
            foreach (var invoice in invoices)
            {
                invoice.DeleteDate = System.DateTime.Now;
            }

            return true;
        }

        
    }
}