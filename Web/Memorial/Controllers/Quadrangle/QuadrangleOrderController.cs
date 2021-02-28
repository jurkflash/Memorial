using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Quadrangle;
using Memorial.Lib.Deceased;
using Memorial.Lib.FuneralCo;
using Memorial.Lib.Applicant;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;

namespace Memorial.Controllers
{
    public class QuadrangleOrderController : Controller
    {
        private IQuadrangle _quadrangle;
        private IDeceased _deceased;
        private IFuneralCo _funeralCo;
        private IOrder _order;
        private IApplicant _applicant;

        private Lib.Invoice.IQuadrangle _quadrangleInvoice;
        private Lib.Receipt.IQuadrangle _quadrangleReceipt;

        public QuadrangleOrderController(
            IQuadrangle quadrangle,
            IApplicant applicant,
            IDeceased deceased, 
            IFuneralCo funeralCo, 
            IOrder order,
            
            Lib.Invoice.IQuadrangle quadrangleInvoice,
            Lib.Receipt.IQuadrangle quadrangleReceipt
            )
        {
            _quadrangle = quadrangle;
            _applicant = applicant;
            _deceased = deceased;
            _funeralCo = funeralCo;
            _order = order;

            _quadrangleInvoice = quadrangleInvoice;
            _quadrangleReceipt = quadrangleReceipt;
        }
        public ActionResult Index(int itemId, int id, int applicantId)
        {
            _quadrangle.SetQuadrangle(id);

            var viewModel = new QuadrangleItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                QuadrangleItemId = itemId,
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                QuadrangleId = id,
                QuadrangleTransactionDtos = _order.GetTransactionDtosByQuadrangleIdAndItemId(id, itemId),
                AllowNew = !_quadrangle.HasApplicant()
            };
            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _order.SetTransaction(AF);
            _quadrangle.SetQuadrangle(_order.GetQuadrangleId());
            var viewModel = new QuadrangleTransactionsInfoViewModel()
            {
                ApplicantId = _order.GetApplicantId(),
                DeceasedId = _order.GetDeceasedId(),
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                ItemName = _order.GetItemName(),
                QuadrangleTransactionDto = _order.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId, int id, int applicantId)
        {
            _quadrangle.SetQuadrangle(id);
            _applicant.SetApplicant(applicantId);
            _order.SetOrder(itemId);

            var quadrangleTransactionDto = new QuadrangleTransactionDto(itemId, id, applicantId);
            quadrangleTransactionDto.Quadrangle = _quadrangle.GetQuadrangle();
            quadrangleTransactionDto.QuadrangleId = id;
            quadrangleTransactionDto.Applicant = _applicant.GetApplicant();
            var viewModel = new QuadrangleTransactionsFormViewModel()
            {
                FuneralCompanyDtos = _funeralCo.GetFuneralCompanyDtos(),
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId),
                QuadrangleTransactionDto = quadrangleTransactionDto
            };
            viewModel.QuadrangleTransactionDto.Price = _quadrangle.GetPrice();
            viewModel.QuadrangleTransactionDto.Maintenance = _quadrangle.GetMaintenance();
            viewModel.QuadrangleTransactionDto.LifeTimeMaintenance = _quadrangle.GetLifeTimeMaintenance();
            return View(viewModel);
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            if (viewModel.QuadrangleTransactionDto.DeceasedId != null)
            {
                _deceased.SetDeceased((int)viewModel.QuadrangleTransactionDto.DeceasedId);
                if (_deceased.GetQuadrangle() != null)
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.DeceasedId", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            _order.SetTransaction(AutoMapper.Mapper.Map<Core.Dtos.QuadrangleTransactionDto, Core.Domain.QuadrangleTransaction>(viewModel.QuadrangleTransactionDto));
            if(_order.Create())
            {
                return RedirectToAction("Index", new
                {
                    itemId = viewModel.QuadrangleTransactionDto.QuadrangleItemId,
                    id = viewModel.QuadrangleTransactionDto.QuadrangleId,
                    applicantId = viewModel.QuadrangleTransactionDto.ApplicantId
                });
            }
            else
            {
                return FormForResubmit(viewModel);
            }
        }

        public ActionResult FormForResubmit(QuadrangleTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = _funeralCo.GetFuneralCompanyDtos();
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.QuadrangleTransactionDto.ApplicantId);

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _order.SetTransaction(AF);
            _order.Delete();
            return RedirectToAction("Index", new
            {
                itemId,
                id,
                applicantId
            });
        }




        public ActionResult Invoices(string AF)
        {
            var viewModel = new InvoicesViewModel()
            {
                AF = AF,
                InvoiceDtos = _quadrangleInvoice.GetInvoiceDtosByAF(AF),
            };

            return View(viewModel);
        }

        public ActionResult NewInvoice(string AF)
        {
            _order.SetTransaction(AF);

            var viewModel = new InvoiceFormViewModel()
            {
                AF = AF,
                Amount = _order.GetAmount(),
                InvoiceDto = new InvoiceDto(),
            };

            return View("InvoiceForm", viewModel);
        }

        public ActionResult SaveInvoice(InvoiceFormViewModel viewModel)
        {
            if (viewModel.Amount < viewModel.InvoiceDto.Amount)
            {
                ModelState.AddModelError("InvoiceDto.Amount", "Amount invalid");
                return View("InvoiceForm", viewModel);
            }

            if (viewModel.InvoiceDto.IV == null)
            {
                _order.SetTransaction(viewModel.AF);
                if (_quadrangleInvoice.Create(_order.GetItemId(), viewModel.AF, viewModel.InvoiceDto.Amount, viewModel.InvoiceDto.Remark))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                {
                    return View("InvoiceForm", viewModel);
                }
            }
            else
            {
                _quadrangleInvoice.SetInvoice(viewModel.InvoiceDto.IV);
                if (_quadrangleInvoice.Update(viewModel.InvoiceDto.Amount, viewModel.InvoiceDto.Remark))
                    return RedirectToAction("Index", new { AF = viewModel.AF });
                else
                    return View("InvoiceForm", viewModel);
            }
        }
    }
}