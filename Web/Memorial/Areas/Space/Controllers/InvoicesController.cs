using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.Lib.Space;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;
using System.Linq;

namespace Memorial.Areas.Space.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly Lib.Invoice.ISpace _invoice;
        private readonly Lib.Receipt.ISpace _receipt;

        public InvoicesController(
            ITransaction transaction,
            Lib.Invoice.ISpace invoice,
            Lib.Receipt.ISpace receipt)
        {
            _transaction = transaction;
            _invoice = invoice;
            _receipt = receipt;
        }

        public ActionResult Index(string AF)
        {
            var viewModel = new InvoicesViewModel()
            {
                AF = AF,
                InvoiceDtos = Mapper.Map<IEnumerable<InvoiceDto>>(_invoice.GetByAF(AF))
            };

            return View(viewModel);
        }

        public ActionResult Info(string IV, bool exportToPDF = false)
        {
            var invoice = _invoice.GetByIV(IV);

            var tansaction = _transaction.GetByAF(invoice.SpaceTransactionAF);

            var viewModel = new InvoiceInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.SummaryItem = tansaction.SummaryItem;
            viewModel.InvoiceDto = Mapper.Map<InvoiceDto>(invoice);
            viewModel.Header = tansaction.SpaceItem.Space.Site.Header;

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
            var tansaction = _transaction.GetByAF(AF);

            var viewModel = new InvoiceFormViewModel()
            {
                AF = AF,
                AllowDeposit = tansaction.SpaceItem.AllowDeposit,
                Amount = _transaction.GetTotalAmount(tansaction)
            };

            if (IV == null)
            {
                viewModel.InvoiceDto = new InvoiceDto();
            }
            else
            {
                viewModel.InvoiceDto = Mapper.Map<InvoiceDto>(_invoice.GetInvoice(IV));
            }

            return View("Form", viewModel);
        }

        public ActionResult Save(InvoiceFormViewModel viewModel)
        {
            var tansaction = _transaction.GetByAF(viewModel.AF);
            if (viewModel.Amount < viewModel.InvoiceDto.Amount)
            {
                viewModel.Amount = _transaction.GetTotalAmount(tansaction);

                ModelState.AddModelError("InvoiceDto.Amount", "Amount invalid");
                return View("Form", viewModel);
            }

            var invoice = Mapper.Map<Core.Domain.Invoice>(viewModel.InvoiceDto);
            if (viewModel.InvoiceDto.IV == null)
            {
                invoice.AllowDeposit = tansaction.SpaceItem.AllowDeposit;
                invoice.SpaceTransactionAF = viewModel.AF;

                if (_invoice.Add(tansaction.SpaceItem.Id, invoice))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                {
                    viewModel.Amount = _transaction.GetTotalAmount(tansaction);
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.Change(invoice.IV, invoice))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                    return View("Form", viewModel);
            }
        }

        public ActionResult Receipt(string IV, string AF)
        {
            return RedirectToAction("Index", "Receipts", new { IV = IV, AF = AF, area = "Space" });
        }

        public ActionResult Delete(string IV, string AF)
        {
            var invoice = _invoice.GetByIV(IV);
            var status = _receipt.GetReceiptsByInvoiceIV(IV).Any();
            var result = _invoice.Remove(invoice);

            return RedirectToAction("Index", new { AF = AF });
        }
    }
}