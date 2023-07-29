﻿using System.Linq;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Columbarium;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using System.Collections.Generic;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly Lib.Invoice.IColumbarium _invoice;
        private readonly Lib.Receipt.IColumbarium _receipt;
        private readonly IPaymentMethod _paymentMethod;
        private readonly IPayment _payment;

        public ReceiptsController(
            ITransaction transaction,
            Lib.Invoice.IColumbarium invoice,
            Lib.Receipt.IColumbarium receipt,
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
                ReceiptDtos = _receipt.GetOrderReceiptDtosByInvoiceIV(IV).OrderByDescending(r => r.CreatedUtcTime)
            };

            return View(viewModel);
        }

        public ActionResult Info(string RE, bool exportToPDF = false)
        {
            _receipt.SetReceipt(RE);
            _invoice.SetInvoice(_receipt.GetInvoiceIV());
            _transaction.SetTransaction(_invoice.GetAF());

            var viewModel = new OrderReceiptInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ReceiptDto = _receipt.GetReceiptDto();
            viewModel.InvoiceDto = viewModel.ReceiptDto.InvoiceDto;

            viewModel.SummaryItem = _transaction.GetTransactionSummaryItem();
            viewModel.Header = _transaction.GetSiteHeader();

            return View(viewModel);
        }

        public ActionResult PrintAll(string RE)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            var report = new Rotativa.ActionAsPdf("Info", new { RE = RE, exportToPDF = true });
            report.Cookies = cookieCollection;

            return report;
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

            viewModel.ReceiptDto.ColumbariumTransactionAF = viewModel.AF;

            if (viewModel.ReceiptDto.RE == null)
            {
                viewModel.ReceiptDto.InvoiceDtoIV = viewModel.InvoiceDto.IV;
                
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