using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Lib;
using Memorial.Lib.Space;
using Memorial.ViewModels;

namespace Memorial.Areas.Space.Controllers
{
    public class NonOrderReceiptsController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly Lib.Receipt.ISpace _receipt;
        private readonly IPaymentMethod _paymentMethod;

        public NonOrderReceiptsController(
            ITransaction transaction,
            Lib.Receipt.ISpace receipt,
            IPaymentMethod paymentMethod)
        {
            _transaction = transaction;
            _receipt = receipt;
            _paymentMethod = paymentMethod;
        }

        public ActionResult Index(string AF)
        {
            var transaction = _transaction.GetByAF(AF);
            var viewModel = new NonOrderReceiptsViewModel()
            {
                AF = AF,
                AFTotalAmount = _transaction.GetTotalAmount(transaction),
                AFTotalAmountPaid = _receipt.GetTotalIssuedReceiptAmount(AF),
                Amount = _transaction.GetTotalAmount(transaction),
                RemainingAmount = _transaction.GetTotalAmount(transaction) - _receipt.GetTotalIssuedReceiptAmount(AF),
                ReceiptDtos = Mapper.Map<IEnumerable<ReceiptDto>>(_receipt.GetByAF(AF).OrderByDescending(r => r.CreatedUtcTime))
            };

            return View(viewModel);
        }

        public ActionResult Info(string RE, bool exportToPDF = false)
        {
            var receipt = _receipt.GetByRE(RE);
            var transaction = _transaction.GetByAF(receipt.SpaceTransactionAF);

            var viewModel = new NonOrderReceiptInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ReceiptDto = Mapper.Map<ReceiptDto>(receipt);

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

        public ActionResult Form(string AF)
        {
            var transaction = _transaction.GetByAF(AF);
            var viewModel = new NewNonOrderReceiptFormViewModel()
            {
                AF = AF,
                Amount = _transaction.GetTotalAmount(transaction),
                RemainingAmount = _transaction.GetTotalAmount(transaction) - _receipt.GetTotalIssuedReceiptAmount(AF),
                PaymentMethods = _paymentMethod.GetAll()
            };
            return View(viewModel);
        }

        public ActionResult Save(NewNonOrderReceiptFormViewModel viewModel)
        {
            var transaction = _transaction.GetByAF(viewModel.AF);
            var receipt = Mapper.Map<Core.Domain.Receipt>(viewModel.ReceiptDto);

            viewModel.Amount = _transaction.GetTotalAmount(transaction);
            viewModel.RemainingAmount = _transaction.GetTotalAmount(transaction) - _receipt.GetTotalIssuedReceiptAmount(viewModel.AF);
            viewModel.PaymentMethods = _paymentMethod.GetAll();

            if (viewModel.ReceiptDto.Amount > viewModel.RemainingAmount)
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
                if (_receipt.Add(transaction.SpaceItemId, receipt))
                {
                    return RedirectToAction("Index", new { AF = viewModel.AF });
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

            return RedirectToAction("Index", new { AF = viewModel.AF });
        }

        public ActionResult Delete(string RE, string AF)
        {
            var receipt = _receipt.GetByRE(RE);
            var status = _receipt.Remove(receipt);

            return RedirectToAction("Index", new { AF = AF });
        }
    }
}