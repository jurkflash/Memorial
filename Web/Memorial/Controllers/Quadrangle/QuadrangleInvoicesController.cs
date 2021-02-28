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
using Memorial.Lib.Quadrangle;
using Memorial.Lib.Invoice;
using AutoMapper;

namespace Memorial.Controllers
{
    public class QuadrangleInvoicesController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly Lib.Invoice.IQuadrangle _invoice;

        public QuadrangleInvoicesController(
            Lib.Invoice.IQuadrangle invoice
            )
        {
            _invoice = invoice;
        }

        public ActionResult Index(string AF)
        {
            var viewModel = new InvoicesViewModel()
            {
                AF = AF,
                InvoiceDtos = _invoice.GetInvoiceDtosByAF(AF),
            };

            return View(viewModel);
        }

        public ActionResult New(string AF)
        {
            _transaction.SetTransaction(AF);
            
            var viewModel = new InvoiceFormViewModel()
            {
                AF = AF,
                Amount = _transaction.GetAmount(),
                InvoiceDto = new InvoiceDto(),
            };

            return View("Form", viewModel);
        }

        public ActionResult Save(InvoiceFormViewModel viewModel)
        {
            if (viewModel.Amount < viewModel.InvoiceDto.Amount)
            {
                ModelState.AddModelError("InvoiceDto.Amount", "Amount invalid");
                return View("Form", viewModel);
            }

            if (viewModel.InvoiceDto.IV == null)
            {
                _transaction.SetTransaction(viewModel.AF);
                if (_invoice.Create(_transaction.GetItemId(), viewModel.AF, viewModel.InvoiceDto.Amount, viewModel.InvoiceDto.Remark))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                _invoice.SetInvoice(viewModel.InvoiceDto.IV);
                if (_invoice.Update(viewModel.InvoiceDto.Amount, viewModel.InvoiceDto.Remark))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                    return View("Form", viewModel);
            }
        }

        public ActionResult Receipt(string IV, string AF, MasterCatalog masterCatalog)
        {
            return RedirectToAction("Index", "OrderReceipts", new { IV = IV, AF = AF, masterCatalog = masterCatalog });
        }

        public ActionResult Delete(string IV)
        {
            _invoice.SetInvoice(IV);
            _invoice.Delete();
            
            return RedirectToAction("Index", new { AF = _invoice.GetAF() });
        }
    }
}