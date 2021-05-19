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
            _invoice.CreateDate = System.DateTime.Now;

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



        //protected IEnumerable<Core.Domain.Invoice> GetInvoicesByAncestorAF(string AF)
        //{
        //    return _unitOfWork.Invoices.GetByActiveAncestorAF(AF);
        //}

        //protected IEnumerable<InvoiceDto> GetInvoiceDtosByAncestorAF(string AF)
        //{
        //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(GetInvoicesByAncestorAF(AF));
        //}

        //protected IEnumerable<Core.Domain.Invoice> GetInvoicesByCremationAF(string AF)
        //{
        //    return _unitOfWork.Invoices.GetByActiveCremationAF(AF);
        //}

        //protected IEnumerable<InvoiceDto> GetInvoiceDtosByCremationAF(string AF)
        //{
        //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(GetInvoicesByCremationAF(AF));
        //}

        //protected IEnumerable<Core.Domain.Invoice> GetInvoicesByMiscellaneousAF(string AF)
        //{
        //    return _unitOfWork.Invoices.GetByActiveMiscellaneousAF(AF);
        //}

        //protected IEnumerable<InvoiceDto> GetInvoiceDtosByMiscellaneousAF(string AF)
        //{
        //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(GetInvoicesByMiscellaneousAF(AF));
        //}

        //protected IEnumerable<Core.Domain.Invoice> GetInvoicesByPlotAF(string AF)
        //{
        //    return _unitOfWork.Invoices.GetByActivePlotAF(AF);
        //}

        //protected IEnumerable<InvoiceDto> GetInvoiceDtosByPlotAF(string AF)
        //{
        //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(GetInvoicesByPlotAF(AF));
        //}

        //protected IEnumerable<Core.Domain.Invoice> GetInvoicesBySpaceAF(string AF)
        //{
        //    return _unitOfWork.Invoices.GetByActiveSpaceAF(AF);
        //}

        //protected IEnumerable<InvoiceDto> GetInvoiceDtosBySpaceAF(string AF)
        //{
        //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(GetInvoicesBySpaceAF(AF));
        //}

        //protected IEnumerable<Core.Domain.Invoice> GetInvoicesByUrnAF(string AF)
        //{
        //    return _unitOfWork.Invoices.GetByActiveUrnAF(AF);
        //}

        //protected IEnumerable<InvoiceDto> GetInvoiceDtosByUrnAF(string AF)
        //{
        //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(GetInvoicesByUrnAF(AF));
        //}




















        //public IEnumerable<InvoiceDto> GetInvoiceDtosByAF(string AF)
        //{
        //    return null;
        //}

        //public InvoiceDto GetDto(string IV)
        //{
        //    return Mapper.Map<Core.Domain.Invoice, InvoiceDto>(_unitOfWork.Invoices.GetByActiveIV(IV));
        //}

        //public float GetAmountByAF(string AF, MasterCatalog masterCatalog)
        //{
        //    switch (masterCatalog)
        //    {
        //        case MasterCatalog.Ancestor:
        //            IAncestor ancestor = new Lib.Ancestor(_unitOfWork);
        //            return ancestor.GetAmount(AF);
        //        case MasterCatalog.Cremation:
        //            ICremation cremation = new Lib.Cremation(_unitOfWork);
        //            return cremation.GetAmount(AF);
        //        case MasterCatalog.Miscellaneous:
        //            IMiscellaneousTransaction miscellaneousTransaction = new Lib.MiscellaneousTransaction(_unitOfWork);
        //            return miscellaneousTransaction.GetAmount(AF);
        //        //case MasterCatalog.Plot:
        //        //    return _unitOfWork.Invoices.GetByActivePlotAF(AF));
        //        case MasterCatalog.Quadrangle:
        //            //ITransaction quadrangleTransaction = new Lib.Quadrangle.Transaction(_unitOfWork);
        //            //quadrangleTransaction.SetByAF(AF);
        //            //return quadrangleTransaction.GetAmount();
        //            return 0;
        //        case MasterCatalog.Space:
        //            ISpaceTransaction spaceTransaction = new Lib.SpaceTransaction(_unitOfWork);
        //            return spaceTransaction.GetAmount(AF);
        //        case MasterCatalog.Urn:
        //            IUrn urn = new Lib.Urn(_unitOfWork);
        //            return urn.GetAmount(AF);
        //        default:
        //            return -1;
        //    }
        //}

        //public IEnumerable<InvoiceDto> GetDtosByAF(string AF, MasterCatalog masterCatalog)
        //{
        //    switch (masterCatalog)
        //    {
        //        case MasterCatalog.Ancestor:
        //            return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActiveAncestorAF(AF));
        //        case MasterCatalog.Cremation:
        //            return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActiveCremationAF(AF));
        //        case MasterCatalog.Miscellaneous:
        //            return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActiveMiscellaneousAF(AF));
        //        case MasterCatalog.Plot:
        //            return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActivePlotAF(AF));
        //        case MasterCatalog.Quadrangle:
        //            return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActiveQuadrangleAF(AF));
        //        case MasterCatalog.Space:
        //            return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActiveSpaceAF(AF));
        //        case MasterCatalog.Urn:
        //            return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActiveUrnAF(AF));
        //        default:
        //            return null;
        //    }
        //}

        //public IEnumerable<Core.Domain.Invoice> GetByAF(string AF, MasterCatalog masterCatalog)
        //{
        //    switch (masterCatalog)
        //    {
        //        case MasterCatalog.Ancestor:
        //            return _unitOfWork.Invoices.GetByActiveAncestorAF(AF);
        //        case MasterCatalog.Cremation:
        //            return _unitOfWork.Invoices.GetByActiveCremationAF(AF);
        //        case MasterCatalog.Miscellaneous:
        //            return _unitOfWork.Invoices.GetByActiveMiscellaneousAF(AF);
        //        case MasterCatalog.Plot:
        //            return _unitOfWork.Invoices.GetByActivePlotAF(AF);
        //        case MasterCatalog.Quadrangle:
        //            return _unitOfWork.Invoices.GetByActiveQuadrangleAF(AF);
        //        case MasterCatalog.Space:
        //            return _unitOfWork.Invoices.GetByActiveSpaceAF(AF);
        //        case MasterCatalog.Urn:
        //            return _unitOfWork.Invoices.GetByActiveUrnAF(AF);
        //        default:
        //            return null;
        //    }
        //}

        //public bool Create(string AF, float amount, string remark, MasterCatalog masterCatalog)
        //{
        //    string IVCode = "";
        //    var invoice = new Core.Domain.Invoice();
        //    switch(masterCatalog)
        //    {
        //        case MasterCatalog.Miscellaneous:
        //            IVCode = _unitOfWork.MiscellaneousNumbers.GetNewIV(_unitOfWork.MiscellaneousTransactions.GetActive(AF).MiscellaneousItemId, System.DateTime.Now.Year);
        //            invoice.MiscellaneousTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Urn:
        //            IVCode = _unitOfWork.UrnNumbers.GetNewIV(_unitOfWork.UrnTransactions.GetActive(AF).UrnItemId, System.DateTime.Now.Year);
        //            invoice.UrnTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Quadrangle:
        //            IVCode = _unitOfWork.QuadrangleNumbers.GetNewIV(_unitOfWork.QuadrangleTransactions.GetActive(AF).QuadrangleItemId, System.DateTime.Now.Year);
        //            invoice.QuadrangleTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Ancestor:
        //            IVCode = _unitOfWork.AncestorNumbers.GetNewIV(_unitOfWork.AncestorTransactions.GetActive(AF).AncestorItemId, System.DateTime.Now.Year);
        //            invoice.AncestorTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Space:
        //            IVCode = _unitOfWork.SpaceNumbers.GetNewIV(_unitOfWork.SpaceTransactions.GetActive(AF).SpaceItemId, System.DateTime.Now.Year);
        //            invoice.SpaceTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Cremation:
        //            IVCode = _unitOfWork.CremationNumbers.GetNewIV(_unitOfWork.CremationTransactions.GetActive(AF).CremationItemId, System.DateTime.Now.Year);
        //            invoice.CremationTransactionAF = AF;
        //            break;
        //    }

        //    if (IVCode == "")
        //        return false;

        //    invoice.IV = IVCode;
        //    invoice.Amount = amount;
        //    invoice.Remark = remark;
        //    invoice.CreateDate = System.DateTime.Now;
        //    _unitOfWork.Invoices.Add(invoice);
        //    _unitOfWork.Complete();

        //    return true;
        //}

        //public bool Update(string IV, float amount, string remark)
        //{
        //    var invoice = _unitOfWork.Invoices.GetByActiveIV(IV);
        //    if (invoice == null)
        //        return false;
        //    else
        //    {
        //        if(invoice.Amount != amount)
        //        {
        //            if (CheckInvoiceAmountToIssuedReceipt(IV, amount))
        //                invoice.Amount = amount;
        //            else
        //                return false;
        //        }

        //        invoice.Remark = remark;
        //        invoice.ModifyDate = System.DateTime.Now;
        //        _unitOfWork.Complete();
        //        return true;
        //    }
        //}

        //public bool UpdateHasReceipt(string IV)
        //{
        //    IReceipt iReceipt = new Lib.Receipt(_unitOfWork);
        //    var invoice = _unitOfWork.Invoices.GetByActiveIV(IV);
        //    if (invoice == null)
        //        return false;

        //    if (iReceipt.GetByIV(IV).Any())
        //    {
        //        invoice.hasReceipt = true;
        //    }
        //    else
        //    {
        //        invoice.hasReceipt = false;
        //    }
        //    _unitOfWork.Complete();
        //    return true;
        //}

        //public bool UpdateIsPaid(string IV)
        //{
        //    IReceipt iReceipt = new Lib.Receipt(_unitOfWork);
        //    var invoice = _unitOfWork.Invoices.GetByActiveIV(IV);
        //    if (invoice == null)
        //        return false;

        //    if (iReceipt.GetByIV(IV).Sum(r => r.Amount) == invoice.Amount)
        //    {
        //        invoice.isPaid = true;
        //    }
        //    else
        //    {
        //        invoice.isPaid = false;
        //    }
        //    _unitOfWork.Complete();
        //    return true;
        //}

        //public bool Delete(string IV)
        //{
        //    IReceipt iReceipt = new Lib.Receipt(_unitOfWork);
        //    var receipts = iReceipt.GetByIV(IV);
        //    foreach (var receipt in receipts)
        //    {
        //        if (!iReceipt.Delete(receipt.RE))
        //            return false;
        //    }

        //    var invoice = _unitOfWork.Invoices.GetByActiveIV(IV);
        //    if (invoice == null)
        //        return false;

        //    invoice.DeleteDate = System.DateTime.Now;
        //    _unitOfWork.Complete();
        //    return true;
        //}

        //public bool CheckInvoiceAmountToIssuedReceipt(string IV, float amount)
        //{
        //    IReceipt iReceipt = new Lib.Receipt(_unitOfWork);
        //    var receipts = iReceipt.GetDtosByIV(IV);
        //    if (receipts.Sum(r => r.Amount) >= amount)
        //        return false;
        //    else
        //        return true;
        //}

        //public float GetUnpaidAmount(string IV)
        //{
        //    IReceipt iReceipt = new Lib.Receipt(_unitOfWork);
        //    var invoiceAmount = _unitOfWork.Invoices.GetByActiveIV(IV).Amount;
        //    var issuedReceiptsTotalAmount = iReceipt.GetDtosByIV(IV).Sum(r => r.Amount);
        //    return invoiceAmount - issuedReceiptsTotalAmount;
        //}
    }
}