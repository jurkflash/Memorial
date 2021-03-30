using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Lib.Plot;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Plot
{
    public class Payment : IPayment
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransaction _transaction;
        private readonly Invoice.IPlot _invoice;
        private readonly Receipt.IPlot _receipt;
        private readonly IPaymentMethod _paymentMethod;

        public Payment(
            IUnitOfWork unitOfWork, 
            ITransaction transaction, 
            Invoice.IPlot invoice, 
            Receipt.IPlot receipt,
            IPaymentMethod paymentMethod
            )
        {
            _unitOfWork = unitOfWork;
            _transaction = transaction;
            _invoice = invoice;
            _receipt = receipt;
            _paymentMethod = paymentMethod;
        }

        public void SetTransaction(string AF)
        {
            _transaction.SetTransaction(AF);
        }

        public void SetInvoice(string IV)
        {
            _invoice.SetInvoice(IV);
        }

        public void SetReceipt(string RE)
        {
            _receipt.SetReceipt(RE);
        }

        public bool DeleteTransaction()
        {
            if (_transaction.IsItemOrder())
            {
                if (_invoice.HasInvoiceByAF(_transaction.GetTransactionAF()))
                {
                    var invoices = _invoice.GetInvoicesByAF(_transaction.GetTransactionAF());

                    foreach (var invoice in invoices)
                    {
                        _receipt.DeleteOrderReceiptsByInvoiceIV(invoice.IV);

                        _invoice.SetInvoice(invoice.IV);
                        _invoice.Delete();
                    }
                }
            }
            else
            {
                _receipt.DeleteNonOrderReceiptsByApplicationAF(_transaction.GetTransactionAF());
            }

            _unitOfWork.Complete();

            return true;
        }

        public bool DeleteInvoice()
        {
            _invoice.Delete();

            _receipt.DeleteOrderReceiptsByInvoiceIV(_invoice.GetIV());

            _unitOfWork.Complete();

            return true;
        }

        public bool DeleteReceipt()
        {
            _receipt.Delete();

            if (_receipt.isOrderReceipt())
            {
                _invoice.SetInvoice(_receipt.GetInvoiceIV());

                _invoice.SetIsPaid(false);

                if (_receipt.GetOrderReceiptsByInvoiceIV(_receipt.GetInvoiceIV()).Count() == 1)
                {
                    _invoice.SetHasReceipt(false);                   
                }
            }

            _unitOfWork.Complete();

            return true;
        }

        public bool UpdateInvoice(InvoiceDto invoiceDto)
        {
            if (_transaction.GetTransactionAmount() < invoiceDto.Amount)
                return false;

            if (_receipt.GetTotalIssuedOrderReceiptAmountByInvoiceIV(invoiceDto.IV) > invoiceDto.Amount)
                return false;

            if (_receipt.GetTotalIssuedOrderReceiptAmountByInvoiceIV(invoiceDto.IV) == invoiceDto.Amount)
            {
                invoiceDto.isPaid = true;
            }
            else
            {
                invoiceDto.isPaid = false;
            }

            _invoice.Update(invoiceDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool UpdateReceipt(ReceiptDto receiptDto)
        {
            if (_paymentMethod.GetPaymentMethod(receiptDto.PaymentMethodId).RequireRemark && receiptDto.PaymentRemark == "")
                return false;

            if (_transaction.IsItemOrder())
            {
                _invoice.SetInvoice(receiptDto.InvoiceIV);

                _receipt.SetReceipt(receiptDto.RE);

                if (_invoice.GetAmount() < _receipt.GetTotalIssuedOrderReceiptAmountByInvoiceIV(receiptDto.InvoiceIV) - _receipt.GetAmount() + receiptDto.Amount)
                    return false;
            }
            else
            {
                if(_transaction.GetTransactionAmount() < _receipt.GetTotalIssuedNonOrderReceiptAmount(_transaction.GetTransactionAF()) - _receipt.GetAmount() + receiptDto.Amount)
                    return false;
            }

            _receipt.Update(receiptDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool CreateInvoice(InvoiceDto invoiceDto)
        {
            _invoice.Create(_transaction.GetItemId(), invoiceDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool CreateReceipt(ReceiptDto receiptDto)
        {
            _receipt.Create(_transaction.GetItemId(), receiptDto);

            if (_transaction.IsItemOrder())
            {
                _invoice.SetInvoice(receiptDto.InvoiceIV);

                _invoice.SetHasReceipt(true);

                if (_invoice.GetAmount() == _receipt.GetTotalIssuedOrderReceiptAmountByInvoiceIV(_invoice.GetIV()) + receiptDto.Amount)
                    _invoice.SetIsPaid(true);
            }

            _unitOfWork.Complete();

            return true;
        }

        public float GetInvoiceUnpaidAmount()
        {
            return _invoice.GetAmount() - _receipt.GetTotalIssuedOrderReceiptAmountByInvoiceIV(_invoice.GetIV());
        }

        public float GetNonOrderTransactionUnpaidAmount()
        {
            return _transaction.GetTransactionAmount() - _receipt.GetTotalIssuedNonOrderReceiptAmount(_transaction.GetTransactionAF());
        }
    }
}