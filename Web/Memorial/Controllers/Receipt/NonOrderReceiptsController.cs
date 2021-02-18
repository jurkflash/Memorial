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
    public class NonOrderReceiptsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoice _invoice;
        private readonly IReceipt _receipt;

        public NonOrderReceiptsController(IUnitOfWork unitOfWork, IInvoice invoice, IReceipt receipt)
        {
            _unitOfWork = unitOfWork;
            _invoice = invoice;
            _receipt = receipt;
        }

        public ActionResult Index(string AF, MasterCatalog masterCatalog)
        {
            ICommon common = new Lib.Common(_unitOfWork);
            var viewModel = new NonOrderReceiptsViewModel()
            {
                AF = AF,
                Amount = common.GetAmount(AF, masterCatalog),
                RemainingAmount = common.GetNonOrderUnpaidAmount(AF, masterCatalog),
                MasterCatalog = masterCatalog,
                ReceiptDtos = _receipt.GetDtosByAF(AF, masterCatalog).OrderByDescending(r => r.CreateDate)
            };
            return View(viewModel);
        }

        public ActionResult Form(string AF, MasterCatalog masterCatalog)
        {
            ICommon common = new Lib.Common(_unitOfWork);
            var viewModel = new NewNonOrderReceiptFormViewModel()
            {
                MasterCatalog = masterCatalog,
                AF = AF,
                Amount = common.GetAmount(AF, masterCatalog),
                RemainingAmount = common.GetNonOrderUnpaidAmount(AF, masterCatalog),
                PaymentMethods = _unitOfWork.PaymentMethods.GetAll()
            };
            return View(viewModel);
        }

        public ActionResult Save(NewNonOrderReceiptFormViewModel viewModel)
        {
            ICommon common = new Lib.Common(_unitOfWork);
            if (viewModel.ReceiptDto.Amount > common.GetNonOrderUnpaidAmount(viewModel.AF, viewModel.MasterCatalog))
            {
                ModelState.AddModelError("ReceiptDto.Amount", "Amount invalid");
                viewModel.Amount = common.GetAmount(viewModel.AF, viewModel.MasterCatalog);
                viewModel.RemainingAmount = common.GetNonOrderUnpaidAmount(viewModel.AF, viewModel.MasterCatalog);
                viewModel.PaymentMethods = _unitOfWork.PaymentMethods.GetAll();
                return View("Form", viewModel);
            }

            if (viewModel.ReceiptDto.RE == null)
            {
                if (_receipt.CreateNonOrderReceipt(viewModel.AF,
                    viewModel.ReceiptDto.Amount,
                    viewModel.ReceiptDto.Remark,
                    viewModel.ReceiptDto.PaymentMethodId,
                    viewModel.ReceiptDto.PaymentRemark,
                    viewModel.MasterCatalog))
                {
                    return RedirectToAction("Index", new { AF = viewModel.AF, MasterCatalog = viewModel.MasterCatalog });
                }
                else
                {
                    viewModel.Amount = common.GetAmount(viewModel.AF, viewModel.MasterCatalog);
                    viewModel.RemainingAmount = common.GetNonOrderUnpaidAmount(viewModel.AF, viewModel.MasterCatalog);
                    viewModel.PaymentMethods = _unitOfWork.PaymentMethods.GetAll();
                    return View("Form", viewModel);
                }
            }
            else
            {
                return RedirectToAction("Index", new { AF = viewModel.AF, MasterCatalog = viewModel.MasterCatalog });
            }
        }

        public ActionResult DeleteNonOrderReceipt(string RE, string AF, MasterCatalog MasterCatalog)
        {
            _receipt.Delete(RE);
            return RedirectToAction("Index", new { AF = AF, MasterCatalog = MasterCatalog });
        }
    }
}