using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Cremation;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Cremation.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly Lib.Invoice.ICremation _invoice;
        private readonly Lib.Receipt.ICremation _receipt;

        public InvoicesController(
            ITransaction transaction,
            Lib.Invoice.ICremation invoice,
            Lib.Receipt.ICremation receipt)
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

        public ActionResult Form(string AF, string IV = null)
        {
            var tansaction = _transaction.GetByAF(AF);

            var viewModel = new InvoiceFormViewModel()
            {
                AF = AF,
                Amount = _transaction.GetTotalAmount(tansaction)
            };

            if (IV == null)
            {
                viewModel.InvoiceDto = new InvoiceDto();
            }
            else
            {
                viewModel.InvoiceDto = Mapper.Map<InvoiceDto>(_invoice.GetByIV(IV));
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
                invoice.CremationTransactionAF = viewModel.AF;
                if (_invoice.Add(tansaction.CremationItemId, invoice))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                {
                    viewModel.Amount = _transaction.GetTotalAmount(tansaction);
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.Change(viewModel.InvoiceDto.IV, invoice))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                    return View("Form", viewModel);
            }
        }

        public ActionResult Info(string IV, bool exportToPDF = false)
        {
            var invoice = _invoice.GetByIV(IV);

            var transaction = _transaction.GetByAF(invoice.CremationTransactionAF);

            var viewModel = new InvoiceInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.SummaryItem = transaction.SummaryItem;
            viewModel.InvoiceDto = Mapper.Map<InvoiceDto>(invoice);
            viewModel.Header = transaction.CremationItem.Cremation.Site.Header;

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

        public ActionResult Receipt(string IV, string AF)
        {
            return RedirectToAction("Index", "Receipts", new { IV = IV, AF = AF, area = "Cremation" });
        }

        public ActionResult Delete(string IV, string AF)
        {
            var invoice = _invoice.GetByIV(IV);
            var status = _receipt.GetByIV(IV).Any();
            if (!status)
            {
                var result = _invoice.Remove(invoice);
            }

            return RedirectToAction("Index", new { AF = AF });
        }
    }
}