﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Space;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Space.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly Lib.Invoice.ISpace _invoice;
        private readonly Lib.Receipt.ISpace _receipt;
        private readonly IPaymentMethod _paymentMethod;

        public ReceiptsController(
            ITransaction transaction,
            Lib.Invoice.ISpace invoice,
            Lib.Receipt.ISpace receipt,
            IPaymentMethod paymentMethod)
        {
            _transaction = transaction;
            _invoice = invoice;
            _receipt = receipt;
            _paymentMethod = paymentMethod;
        }

        public ActionResult Index(string IV)
        {
            var invoice = _invoice.GetByIV(IV);
            var transaction = _transaction.GetByAF(invoice.SpaceTransactionAF);

            var viewModel = new OrderReceiptsViewModel()
            {
                AF = invoice.SpaceTransactionAF,
                AFTotalAmount = _transaction.GetTotalAmount(transaction),
                AFTotalAmountPaid = _receipt.GetTotalIssuedReceiptAmount(invoice.SpaceTransactionAF),
                RemainingAmount = invoice.Amount - _receipt.GetTotalIssuedReceiptAmountByIV(IV),
                InvoiceDto = Mapper.Map<InvoiceDto>(invoice),
                ReceiptDtos = Mapper.Map<IEnumerable<ReceiptDto>>(_receipt.GetReceiptsByInvoiceIV(IV).OrderByDescending(r => r.CreatedUtcTime))
            };

            return View(viewModel);
        }

        public ActionResult Info(string RE, bool exportToPDF = false)
        {
            var receipt = _receipt.GetByRE(RE);
            var transaction = _transaction.GetByAF(receipt.SpaceTransactionAF);

            var viewModel = new OrderReceiptInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ReceiptDto = Mapper.Map<ReceiptDto>(receipt);
            viewModel.InvoiceDto = Mapper.Map<InvoiceDto>(receipt.Invoice);
            
            viewModel.SummaryItem = transaction.SummaryItem;
            viewModel.Header = transaction.SpaceItem.Space.Site.Header;

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
            var invoice = _invoice.GetByIV(IV);
            var viewModel = new NewOrderReceiptFormViewModel()
            {
                AF = AF,
                RemainingAmount = invoice.Amount - _receipt.GetTotalIssuedReceiptAmountByIV(IV),
                InvoiceDto = Mapper.Map<InvoiceDto>(invoice),
                PaymentMethods = _paymentMethod.GetAll()
            };

            if (RE == null)
            {
                viewModel.ReceiptDto = new ReceiptDto();
            }
            else
            {
                var receipt = _receipt.GetByRE(RE);
                viewModel.ReceiptDto = Mapper.Map<ReceiptDto>(receipt);
            }

            return View(viewModel);
        }

        public ActionResult Save(NewOrderReceiptFormViewModel viewModel)
        {
            var invoice = _invoice.GetByIV(viewModel.InvoiceDto.IV);
            var transaction = _transaction.GetByAF(viewModel.AF);
            var receipt = Mapper.Map<Core.Domain.Receipt>(viewModel.ReceiptDto);

            viewModel.RemainingAmount = _invoice.GetUnpaidAmount(invoice);
            viewModel.InvoiceDto = Mapper.Map<InvoiceDto>(invoice);
            viewModel.PaymentMethods = _paymentMethod.GetAll();

            if (invoice.Amount < viewModel.ReceiptDto.Amount || (viewModel.RemainingAmount < viewModel.ReceiptDto.Amount && viewModel.ReceiptDto.RE == null))
            {
                ModelState.AddModelError("ReceiptDto.Amount", "Amount invalid");
                
                return View("Form", viewModel);
            }

            var totalRemainingAmount = _transaction.GetTotalAmount(transaction) - _receipt.GetTotalIssuedReceiptAmount(viewModel.AF);
            if (viewModel.ReceiptDto.Amount > totalRemainingAmount)
            {
                ModelState.AddModelError("ReceiptDto.Amount", "Amount over total");

                return View("Form", viewModel);
            }

            receipt.SpaceTransactionAF = viewModel.AF;

            if (viewModel.ReceiptDto.RE == null)
            {
                receipt.InvoiceIV = viewModel.InvoiceDto.IV;
                
                if (_receipt.Add(transaction.SpaceItemId, receipt))
                {
                    return RedirectToAction("Index", new { IV = viewModel.InvoiceDto.IV });
                }
                else
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                var status = _receipt.Change(receipt.RE, receipt);
            }

            return RedirectToAction("Index", new { IV = viewModel.InvoiceDto.IV });
        }

        public ActionResult Delete(string RE, string IV, string AF)
        {
            var receipt = _receipt.GetByRE(RE);
            var status = _receipt.Remove(receipt);          

            return RedirectToAction("Index", new { IV = IV });
        }
    }
}