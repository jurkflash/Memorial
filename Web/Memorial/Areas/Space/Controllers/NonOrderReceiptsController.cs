using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Receipt;
using Memorial.Lib.Space;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Space.Controllers
{
    public class NonOrderReceiptsController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly Lib.Receipt.ISpace _receipt;
        private readonly IPaymentMethod _paymentMethod;
        private readonly IPayment _payment;

        public NonOrderReceiptsController(
            ITransaction transaction,
            Lib.Receipt.ISpace receipt,
            IPaymentMethod paymentMethod,
            IPayment payment)
        {
            _transaction = transaction;
            _receipt = receipt;
            _paymentMethod = paymentMethod;
            _payment = payment;
        }

        public ActionResult Index(string AF)
        {
            _transaction.SetTransaction(AF);
            var viewModel = new NonOrderReceiptsViewModel()
            {
                AF = AF,
                Amount = _transaction.GetTransactionTotalAmount(),
                RemainingAmount = _transaction.GetTransactionTotalAmount() - _receipt.GetTotalIssuedNonOrderReceiptAmount(AF),
                ReceiptDtos = _receipt.GetNonOrderReceiptDtos(AF).OrderByDescending(r => r.CreatedDate)
            };

            return View(viewModel);
        }

        public ActionResult Info(string RE, bool exportToPDF = false)
        {
            var viewModel = new NonOrderReceiptInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ReceiptDto = _receipt.GetReceiptDto(RE);

            _transaction.SetTransaction(_receipt.GetApplicationAF());
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

        public ActionResult Form(string AF)
        {
            _transaction.SetTransaction(AF);
            var viewModel = new NewNonOrderReceiptFormViewModel()
            {
                AF = AF,
                Amount = _transaction.GetTransactionTotalAmount(),
                RemainingAmount = _transaction.GetTransactionTotalAmount() - _receipt.GetTotalIssuedNonOrderReceiptAmount(AF),
                PaymentMethods = _paymentMethod.GetPaymentMethods()
            };
            return View(viewModel);
        }

        public ActionResult Save(NewNonOrderReceiptFormViewModel viewModel)
        {
            _transaction.SetTransaction(viewModel.AF);
            _payment.SetTransaction(viewModel.AF);

            if (viewModel.ReceiptDto.Amount > _payment.GetNonOrderTransactionUnpaidAmount())
            {
                ModelState.AddModelError("ReceiptDto.Amount", "Amount invalid");
                viewModel.Amount = _transaction.GetTransactionTotalAmount();
                viewModel.RemainingAmount = _payment.GetNonOrderTransactionUnpaidAmount();
                viewModel.PaymentMethods = _paymentMethod.GetPaymentMethods();
                return View("Form", viewModel);
            }

            _payment.SetTransaction(viewModel.AF);

            viewModel.ReceiptDto.SpaceTransactionAF = viewModel.AF;

            if (viewModel.ReceiptDto.RE == null)
            {
                if (_payment.CreateReceipt(viewModel.ReceiptDto))
                {
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                }
                else
                {
                    viewModel.Amount = _transaction.GetTransactionTotalAmount();
                    viewModel.RemainingAmount = _payment.GetNonOrderTransactionUnpaidAmount();
                    viewModel.PaymentMethods = _paymentMethod.GetPaymentMethods();
                    return View("Form", viewModel);
                }
            }
            else
            {
                _payment.UpdateReceipt(viewModel.ReceiptDto);
            }

            return RedirectToAction("Index", new { AF = viewModel.AF });
        }

        public ActionResult Delete(string RE, string AF)
        {
            _payment.SetReceipt(RE);
            _payment.DeleteReceipt();

            return RedirectToAction("Index", new { AF = AF });
        }
    }
}