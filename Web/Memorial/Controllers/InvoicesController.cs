using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoice _invoice;

        public InvoicesController(IInvoice invoice)
        {
            _invoice = invoice;
        }

        public ActionResult Index(string AF, MasterCatalog masterCatalog)
        {
            var viewModel = new InvoicesViewModel()
            {
                AF = AF,
                InvoiceDtos = _invoice.GetDtosByAF(AF, masterCatalog),
                MasterCatalog = masterCatalog
            };

            return View(viewModel);
        }

        public ActionResult New(string AF, MasterCatalog masterCatalog)
        {
            var viewModel = new InvoiceFormViewModel()
            {
                AF = AF,
                Amount = _invoice.GetAmountByAF(AF, masterCatalog),
                InvoiceDto = new InvoiceDto(),
                MasterCatalog = masterCatalog
            };

            return View("Form", viewModel);
        }

        public ActionResult Save(InvoiceFormViewModel viewModel)
        {
            if (viewModel.Amount < viewModel.InvoiceDto.Amount)
            {
                ModelState.AddModelError("InvoiceDto.Amount", "Amount invalid");
                viewModel.Amount = _invoice.GetAmountByAF(viewModel.AF, viewModel.MasterCatalog);
                return View("Form", viewModel);
            }

            if (viewModel.InvoiceDto.IV == null)
            {
                if (_invoice.Create(viewModel.AF, viewModel.InvoiceDto.Amount, viewModel.InvoiceDto.Remark, viewModel.MasterCatalog))
                    return RedirectToAction("Index", new { AF = viewModel.AF, masterCatalog = viewModel.MasterCatalog });
                else
                {
                    viewModel.Amount = _invoice.GetAmountByAF(viewModel.AF, viewModel.MasterCatalog);
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.Update(viewModel.InvoiceDto.IV, viewModel.InvoiceDto.Amount, viewModel.InvoiceDto.Remark))
                    return RedirectToAction("Index", new { AF = viewModel.AF, masterCatalog = viewModel.MasterCatalog });
                else
                    return View("Form", viewModel);
            }
        }

        public ActionResult Receipt(string IV, string AF, MasterCatalog masterCatalog)
        {
            return RedirectToAction("Index", "OrderReceipts", new { IV = IV, AF = AF, masterCatalog = masterCatalog });
        }

        public ActionResult Delete(string IV, string AF)
        {
            _invoice.Delete(IV);

            return RedirectToAction("Index", new { AF = AF });
        }
    }
}