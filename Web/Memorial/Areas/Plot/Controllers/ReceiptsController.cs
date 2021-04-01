using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Receipt;
using Memorial.Lib.Plot;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Plot.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly Lib.Invoice.IPlot _invoice;
        private readonly Lib.Receipt.IPlot _receipt;
        private readonly IPaymentMethod _paymentMethod;
        private readonly IPayment _payment;

        public ReceiptsController(
            ITransaction transaction,
            Lib.Invoice.IPlot invoice,
            Lib.Receipt.IPlot receipt,
            IPaymentMethod paymentMethod,
            IPayment payment)
        {
            _transaction = transaction;
            _invoice = invoice;
            _receipt = receipt;
            _paymentMethod = paymentMethod;
            _payment = payment;
        }

        public ActionResult Index(string IV)
        {
            _invoice.SetInvoice(IV);

            var viewModel = new OrderReceiptsViewModel()
            {
                AF = _invoice.GetAF(),
                RemainingAmount = _invoice.GetAmount() - _receipt.GetTotalIssuedOrderReceiptAmountByInvoiceIV(IV),
                InvoiceDto = _invoice.GetInvoiceDto(),
                ReceiptDtos = _receipt.GetOrderReceiptDtosByInvoiceIV(IV).OrderByDescending(r => r.CreateDate)
            };

            return View(viewModel);
        }

        public ActionResult Form(string IV, string AF, string RE = null)
        {
            _transaction.SetTransaction(AF);
            _invoice.SetInvoice(IV);

            var viewModel = new NewOrderReceiptFormViewModel()
            {
                AF = AF,
                RemainingAmount = _invoice.GetAmount() - _receipt.GetTotalIssuedOrderReceiptAmountByInvoiceIV(IV),
                InvoiceDto = _invoice.GetInvoiceDto(),
                PaymentMethods = _paymentMethod.GetPaymentMethods()
            };

            if (RE == null)
            {
                viewModel.ReceiptDto = new ReceiptDto();
            }
            else
            {
                viewModel.ReceiptDto = _receipt.GetReceiptDto(RE);
            }

            return View(viewModel);
        }

        public ActionResult Save(NewOrderReceiptFormViewModel viewModel)
        {
            _payment.SetInvoice(viewModel.InvoiceDto.IV);

            if (viewModel.InvoiceDto.Amount < viewModel.ReceiptDto.Amount || (_payment.GetInvoiceUnpaidAmount() < viewModel.ReceiptDto.Amount && viewModel.ReceiptDto.RE == null))
            {
                ModelState.AddModelError("ReceiptDto.Amount", "Amount invalid");
                viewModel.RemainingAmount = _payment.GetInvoiceUnpaidAmount();
                viewModel.InvoiceDto = _invoice.GetInvoiceDto(viewModel.InvoiceDto.IV);
                viewModel.PaymentMethods = _paymentMethod.GetPaymentMethods();
                return View("Form", viewModel);
            }

            _payment.SetTransaction(viewModel.AF);

            viewModel.ReceiptDto.PlotTransactionAF = viewModel.AF;

            if (viewModel.ReceiptDto.RE == null)
            {
                viewModel.ReceiptDto.InvoiceIV = viewModel.InvoiceDto.IV;
                
                if (_payment.CreateReceipt(viewModel.ReceiptDto))
                {
                    return RedirectToAction("Index", new { IV = viewModel.InvoiceDto.IV });
                }
                else
                {
                    viewModel.PaymentMethods = _paymentMethod.GetPaymentMethods();
                    viewModel.RemainingAmount = _payment.GetInvoiceUnpaidAmount();
                    viewModel.InvoiceDto = _invoice.GetInvoiceDto(viewModel.InvoiceDto.IV);
                    return View("Form", viewModel);
                }
            }
            else
            {
                _payment.UpdateReceipt(viewModel.ReceiptDto);
            }

            return RedirectToAction("Index", new { IV = viewModel.InvoiceDto.IV });
        }

        public ActionResult Delete(string RE, string IV, string AF)
        {
            _payment.SetReceipt(RE);
            _payment.DeleteReceipt();

            return RedirectToAction("Index", new { IV = IV });
        }
    }
}