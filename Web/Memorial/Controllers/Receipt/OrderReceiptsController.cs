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
    public class OrderReceiptsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoice _invoice;
        private readonly IReceipt _receipt;

        public OrderReceiptsController(IUnitOfWork unitOfWork, IInvoice invoice, IReceipt receipt)
        {
            _unitOfWork = unitOfWork;
            _invoice = invoice;
            _receipt = receipt;
        }

        public ActionResult Index(string IV, string AF, MasterCatalog masterCatalog)
        {
            var viewModel = new OrderReceiptsViewModel()
            {
                AF = AF,
                RemainingAmount = _invoice.GetUnpaidAmount(IV),
                MasterCatalog = masterCatalog,
                InvoiceDto = _invoice.GetDto(IV),
                ReceiptDtos = _receipt.GetDtosByIV(IV).OrderByDescending(r => r.CreateDate)
            };

            return View(viewModel);
        }

        public ActionResult Form(string IV, string AF, MasterCatalog masterCatalog)
        {
            var viewModel = new NewOrderReceiptFormViewModel()
            {
                MasterCatalog = masterCatalog,
                AF = AF,
                RemainingAmount = _invoice.GetUnpaidAmount(IV),
                InvoiceDto = _invoice.GetDto(IV),
                ReceiptDto = new ReceiptDto(),
                PaymentMethods = _unitOfWork.PaymentMethods.GetAll()
            };
            return View(viewModel);
        }

        public ActionResult Save(NewOrderReceiptFormViewModel viewModel)
        {
            if (viewModel.InvoiceDto.Amount < viewModel.ReceiptDto.Amount || _invoice.GetUnpaidAmount(viewModel.InvoiceDto.IV) < viewModel.ReceiptDto.Amount)
            {
                ModelState.AddModelError("ReceiptDto.Amount", "Amount invalid");
                viewModel.RemainingAmount = _invoice.GetUnpaidAmount(viewModel.InvoiceDto.IV);
                viewModel.InvoiceDto = _invoice.GetDto(viewModel.InvoiceDto.IV);
                viewModel.PaymentMethods = _unitOfWork.PaymentMethods.GetAll();
                return View("Form", viewModel);
            }

            if (viewModel.ReceiptDto.RE == null)
            {
                if (_receipt.CreateOrderReceipt(viewModel.AF, viewModel.InvoiceDto.IV,
                    viewModel.ReceiptDto.Amount,
                    viewModel.ReceiptDto.Remark,
                    viewModel.ReceiptDto.PaymentMethodId,
                    viewModel.ReceiptDto.PaymentRemark,
                    viewModel.MasterCatalog))
                {
                    return RedirectToAction("Index", new { IV = viewModel.InvoiceDto.IV, AF = viewModel.AF, masterCatalog = viewModel.MasterCatalog });
                }
                else
                {
                    viewModel.PaymentMethods = _unitOfWork.PaymentMethods.GetAll();
                    viewModel.RemainingAmount = _invoice.GetUnpaidAmount(viewModel.InvoiceDto.IV);
                    viewModel.InvoiceDto = _invoice.GetDto(viewModel.InvoiceDto.IV);
                    return View("Form", viewModel);
                }
            }
            else
            {
                var receipt = _unitOfWork.Receipts.GetByActiveRE(viewModel.ReceiptDto.RE);
                Mapper.Map(viewModel.ReceiptDto, receipt);
                receipt.ModifyDate = System.DateTime.Now;
                _unitOfWork.Complete();
            }

            return RedirectToAction("Index", new { IV = viewModel.InvoiceDto.IV });
        }

        public ActionResult Delete(string RE, string IV, string AF, MasterCatalog MasterCatalog)
        {
            _receipt.Delete(RE);
            IInvoice invoice = new Lib.Invoice(_unitOfWork);
            invoice.UpdateHasReceipt(IV);

            return RedirectToAction("Index", new { IV = IV, AF = AF, MasterCatalog = MasterCatalog });
        }
    }
}