using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;
using AutoMapper;

namespace Memorial.Lib.Receipt
{
    public abstract class Receipt : IReceipt
    {
        private readonly IUnitOfWork _unitOfWork;
        protected Core.Domain.Receipt _receipt;
        protected string _reNumber;
        protected float _nonOrderAmount;
        protected float _nonOrderTotalIssuedReceiptsAmount;

        public Receipt(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetReceipt(string RE)
        {
            _receipt = _unitOfWork.Receipts.GetByActiveRE(RE);
        }

        public void SetReceipt(Core.Domain.Receipt receipt)
        {
            _receipt = receipt;
        }

        public Core.Domain.Receipt GetReceipt()
        {
            return _receipt;
        }
        public ReceiptDto GetReceiptDto()
        {
            return Mapper.Map<Core.Domain.Receipt, ReceiptDto>(_receipt);
        }

        public Core.Domain.Receipt GetReceipt(string RE)
        {
            return _unitOfWork.Receipts.GetByActiveRE(RE);
        }

        public ReceiptDto GetReceiptDto(string RE)
        {
            return Mapper.Map<Core.Domain.Receipt, ReceiptDto>(GetReceipt(RE));
        }

        public IEnumerable<Core.Domain.Receipt> GetOrderReceiptsByInvoiceIV(string IV)
        {
            return _unitOfWork.Receipts.GetByActiveIV(IV);
        }

        public IEnumerable<ReceiptDto> GetOrderReceiptDtosByInvoiceIV(string IV)
        {
            return Mapper.Map< IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(GetOrderReceiptsByInvoiceIV(IV));
        }

        public string GetInvoiceIV()
        {
            return _receipt.InvoiceIV;
        }

        public float GetAmount()
        {
            return _receipt.Amount;
        }

        public void SetAmount(float amount)
        {
            _receipt.Amount = amount;
        }

        public string GetRemark()
        {
            return _receipt.Remark;
        }

        public void SetRemark(string remark)
        {
            _receipt.Remark = remark;
        }

        public int GetPaymentMethodId()
        {
            return _receipt.PaymentMethodId;
        }

        public int SetPaymentMethodId(byte paymentMethodId)
        {
            return _receipt.PaymentMethodId = paymentMethodId;
        }

        public string GetPaymentRemark()
        {
            return _receipt.PaymentRemark;
        }

        public void SetPaymentRemark(string paymentRemark)
        {
            _receipt.PaymentRemark = paymentRemark;
        }

        public bool isOrderReceipt()
        {
            return _receipt.InvoiceIV == null ? false : true;
        }

        public float GetTotalIssuedOrderReceiptAmountByInvoiceIV(string IV)
        {
            return GetOrderReceiptsByInvoiceIV(IV).Sum(r => r.Amount);
        }

        abstract
        public void NewNumber(int itemId);

        protected bool CreateNewReceipt(ReceiptDto receiptDto)
        {
            if (string.IsNullOrEmpty(_reNumber))
                return false;

            _receipt = new Core.Domain.Receipt();

            Mapper.Map(receiptDto, _receipt);

            _receipt.RE = _reNumber;
            _receipt.CreateDate = System.DateTime.Now;

            _unitOfWork.Receipts.Add(_receipt);

            return true;
        }

        protected bool UpdateReceipt(ReceiptDto receiptDto)
        {
            var receiptInDb = GetReceipt(receiptDto.RE);

            Mapper.Map(receiptDto, receiptInDb);

            receiptInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteReceipt()
        {
            if (_receipt == null)
                return false;

            _receipt.DeleteDate = System.DateTime.Now;

            return true;
        }

        public bool DeleteOrderReceiptsByInvoiceIV(string IV)
        {
            var receipts = GetOrderReceiptsByInvoiceIV(IV);
            foreach(var receipt in receipts)
            {
                receipt.DeleteDate = System.DateTime.Now;
            }

            //_invoice.SetInvoice(_receipt.InvoiceIV);
            //_invoice.SetIsPaid(false);
            //_invoice.SetHasReceipt(false);

            return true;
        }















        //public bool CreateOrderReceipt(string AF, string IV, float amount, string remark, byte paymentMethodId, string paymentRemark, MasterCatalog masterCatalog)
        //{
        //    return CreateReceipt(AF, IV, amount, remark, paymentMethodId, paymentRemark, masterCatalog, true);
        //}

        //public bool CreateNonOrderReceipt(string AF, float amount, string remark, byte paymentMethodId, string paymentRemark, MasterCatalog masterCatalog)
        //{
        //    return CreateReceipt(AF, "", amount, remark, paymentMethodId, paymentRemark, masterCatalog, false);
        //}

        //private bool CreateReceipt(string AF, string IV,  float amount, string remark, byte paymentMethodId, string paymentRemark, MasterCatalog masterCatalog, bool isOrderFlag)
        //{
        //    string RECode = "";
        //    var receipt = new Core.Domain.Receipt();

        //    switch (masterCatalog)
        //    {
        //        case MasterCatalog.Ancestor:
        //            RECode = _unitOfWork.AncestorNumbers.GetNewRE(_unitOfWork.AncestorTransactions.GetActive(AF).AncestorItemId, System.DateTime.Now.Year);
        //            receipt.AncestorTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Cremation:
        //            RECode = _unitOfWork.CremationNumbers.GetNewRE(_unitOfWork.CremationTransactions.GetActive(AF).CremationItemId, System.DateTime.Now.Year);
        //            receipt.CremationTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Miscellaneous:
        //            RECode = _unitOfWork.MiscellaneousNumbers.GetNewRE(_unitOfWork.MiscellaneousTransactions.GetActive(AF).MiscellaneousItemId, System.DateTime.Now.Year);
        //            receipt.MiscellaneousTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Plot:
        //            RECode = _unitOfWork.SpaceNumbers.GetNewRE(_unitOfWork.SpaceTransactions.GetActive(AF).SpaceItemId, System.DateTime.Now.Year);
        //            receipt.SpaceTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Quadrangle:
        //            RECode = _unitOfWork.QuadrangleNumbers.GetNewRE(_unitOfWork.QuadrangleTransactions.GetActive(AF).QuadrangleItemId, System.DateTime.Now.Year);
        //            receipt.QuadrangleTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Space:
        //            RECode = _unitOfWork.SpaceNumbers.GetNewRE(_unitOfWork.SpaceTransactions.GetActive(AF).SpaceItemId, System.DateTime.Now.Year);
        //            receipt.SpaceTransactionAF = AF;
        //            break;
        //        case MasterCatalog.Urn:
        //            RECode = _unitOfWork.UrnNumbers.GetNewRE(_unitOfWork.UrnTransactions.GetActive(AF).UrnItemId, System.DateTime.Now.Year);
        //            receipt.UrnTransactionAF = AF;
        //            break;
        //        default:
        //            break;
        //    }

        //    if (RECode == "")
        //        return false;

        //    if (isOrderFlag)
        //        receipt.InvoiceIV = IV;

        //    receipt.RE = RECode;
        //    receipt.Amount = amount;
        //    receipt.Remark = remark;
        //    receipt.PaymentMethodId = paymentMethodId;
        //    receipt.PaymentRemark = paymentRemark;
        //    receipt.CreateDate = System.DateTime.Now;
        //    _unitOfWork.Receipts.Add(receipt);
        //    _unitOfWork.Complete();

        //    if (isOrderFlag)
        //    {
        //        IInvoice iInvoice = new Lib.Invoice(_unitOfWork);
        //        iInvoice.UpdateHasReceipt(IV);

        //        iInvoice.UpdateIsPaid(IV);
        //    }

        //    return true;
        //}


        
    }
}