using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Cremation;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.FuneralCo;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Controllers
{
    public class CremationOrdersController : Controller
    {
        private readonly ICremation _cremation;
        private readonly IItem _item;
        private readonly IOrder _order;
        private readonly IFuneralCo _funeralCo;
        private readonly IDeceased _deceased;

        public CremationOrdersController(
            ICremation cremation,
            IItem item,
            IOrder order,
            IFuneralCo funeralCo,
            IDeceased deceased
            )
        {
            _cremation = cremation;
            _item = item;
            _order = order;
            _funeralCo = funeralCo;
            _deceased = deceased;
        }

        public ActionResult Index(int itemId, int applicantId)
        {
            var viewModel = new CremationItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                CremationItemId = itemId,
                CremationTransactionDtos = _order.GetTransactionDtosByItemId(itemId),
                AllowNew = true
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            return View(_order.GetTransactionDto(AF));
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var cremationTransactionDto = new CremationTransactionDto();

            var viewModel = new CremationTransactionsFormViewModel()
            {
                FuneralCompanyDtos = _funeralCo.GetFuneralCompanyDtos(),
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            _item.SetItem(itemId);
            _cremation.SetCremation(_item.GetCremationId());

            if (AF == null)
            {
                cremationTransactionDto.ApplicantId = applicantId;
                cremationTransactionDto.CremationItemId = itemId;
                cremationTransactionDto.Price = _item.GetPrice();
                viewModel.CremationTransactionDto = cremationTransactionDto;
            }
            else
            {
                viewModel.CremationTransactionDto = _order.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(CremationTransactionsFormViewModel viewModel)
        {
            if (viewModel.CremationTransactionDto.AF == null)
            {
                if (_order.GetTransactionsByItemIdAndDeceasedId(viewModel.CremationTransactionDto.CremationItemId, viewModel.CremationTransactionDto.DeceasedId).Any())
                {
                    ModelState.AddModelError("CremationTransactionDto.DeceasedId", "Deceased order exists");
                    return FormForResubmit(viewModel);
                }

                if (!_order.Create(viewModel.CremationTransactionDto))
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (!_order.Update(viewModel.CremationTransactionDto))
                {
                    return View("Form", viewModel);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.CremationTransactionDto.CremationItemId,
                applicantId = viewModel.CremationTransactionDto.ApplicantId
            });
        }

        public ActionResult FormForResubmit(CremationTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = _funeralCo.GetFuneralCompanyDtos();
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.CremationTransactionDto.ApplicantId);

            return View("Form", viewModel);
        }


        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _order.SetTransaction(AF);
            _order.Delete();

            return RedirectToAction("Index", new
            {
                itemId,
                applicantId
            });
        }

        public ActionResult Invoices(string AF)
        {
            return RedirectToAction("Index", "CremationInvoices", new { AF = AF });
        }

    }
}