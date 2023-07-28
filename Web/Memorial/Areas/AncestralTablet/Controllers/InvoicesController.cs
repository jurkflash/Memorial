using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Lib.Invoice;
using Memorial.Lib.AncestralTablet;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly Lib.Invoice.IAncestralTablet _invoice;
        private readonly IPayment _payment;

        public InvoicesController(
            ITransaction transaction,
            Lib.Invoice.IAncestralTablet invoice, 
            IPayment payment)
        {
            _transaction = transaction;
            _invoice = invoice;
            _payment = payment;
        }

        public ActionResult Index(string AF)
        {
            var viewModel = new InvoicesViewModel()
            {
                AF = AF,
                InvoiceDtos = _invoice.GetInvoiceDtosByAF(AF)
            };

            return View(viewModel);
        }

        public ActionResult Info(string IV, bool exportToPDF = false)
        {
            _invoice.SetInvoice(IV);

            _transaction.SetTransaction(_invoice.GetAF());

            var viewModel = new InvoiceInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.SummaryItem = _transaction.GetTransactionSummaryItem();
            viewModel.InvoiceDto = _invoice.GetInvoiceDto(IV);
            viewModel.Header = _transaction.GetSiteHeader();

            return View(viewModel);
        }

        public ActionResult PrintAll(string IV)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            var report = new Rotativa.ActionAsPdf("Info", new { IV = IV, exportToPDF = true });
            report.Cookies = cookieCollection;

            return report;
        }

        public ActionResult Form(string AF, string IV = null)
        {
            _transaction.SetTransaction(AF);

            var viewModel = new InvoiceFormViewModel()
            {
                AF = AF,
                Amount = _transaction.GetTransactionAmount()
            };

            if (IV == null)
            {
                viewModel.InvoiceDto = new InvoiceDto();
            }
            else
            {
                viewModel.InvoiceDto = _invoice.GetInvoiceDto(IV);
            }

            return View("Form", viewModel);
        }

        public ActionResult Save(InvoiceFormViewModel viewModel)
        {
            if (viewModel.Amount < viewModel.InvoiceDto.Amount)
            {
                _transaction.SetTransaction(viewModel.AF);

                ModelState.AddModelError("InvoiceDto.Amount", "Amount invalid");
                viewModel.Amount = _transaction.GetTransactionAmount();
                return View("Form", viewModel);
            }

            _payment.SetTransaction(viewModel.AF);

            viewModel.InvoiceDto.AncestralTabletTransactionAF = viewModel.AF;

            if (viewModel.InvoiceDto.IV == null)
            {
                if (_payment.CreateInvoice(viewModel.InvoiceDto))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                {
                    viewModel.Amount = _transaction.GetTransactionAmount();
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_payment.UpdateInvoice(viewModel.InvoiceDto))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                    return View("Form", viewModel);
            }
        }

        public ActionResult Receipt(string IV, string AF)
        {
            return RedirectToAction("Index", "Receipts", new { IV = IV, AF = AF });
        }

        public ActionResult Delete(string IV, string AF)
        {
            _payment.SetInvoice(IV);
            _payment.DeleteInvoice();

            return RedirectToAction("Index", new { AF = AF });
        }
    }
}