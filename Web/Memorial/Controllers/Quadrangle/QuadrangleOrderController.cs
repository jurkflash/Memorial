﻿using System;
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
using AutoMapper;

namespace Memorial.Controllers
{
    public class QuadrangleOrderController : Controller
    {
        private readonly IQuadrangle _quadrangle;
        private readonly IDeceased _deceased;
        private readonly IFuneralCo _funeralCo;
        private readonly IOrder _order;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly IQuadrangleApplicantDeceaseds _quadrangleApplicantDeceaseds;
        private readonly Lib.Invoice.IQuadrangle _invoice;

        public QuadrangleOrderController(
            IQuadrangle quadrangle,
            IApplicant applicant,
            IDeceased deceased, 
            IFuneralCo funeralCo, 
            IOrder order,
            ITracking tracking,
            IQuadrangleApplicantDeceaseds quadrangleApplicantDeceaseds,
            Lib.Invoice.IQuadrangle invoice
            )
        {
            _quadrangle = quadrangle;
            _applicant = applicant;
            _deceased = deceased;
            _funeralCo = funeralCo;
            _order = order;
            _tracking = tracking;
            _quadrangleApplicantDeceaseds = quadrangleApplicantDeceaseds;
            _invoice = invoice;
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
            };

            if(_quadrangle.HasApplicant())
            {
                viewModel.AllowNew = false;
            }
            else
            {
                viewModel.AllowNew = true;
            }

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _order.SetTransaction(AF);
            _quadrangle.SetQuadrangle(_order.GetTransactionQuadrangleId());

            var viewModel = new QuadrangleTransactionsInfoViewModel()
            {
                ApplicantId = _order.GetTransactionApplicantId(),
                DeceasedId = _order.GetTransactionDeceased1Id(),
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                ItemName = _order.GetItemName(),
                QuadrangleTransactionDto = _order.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new QuadrangleTransactionsFormViewModel()
            {
                FuneralCompanyDtos = _funeralCo.GetFuneralCompanyDtos(),
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };
            
            if (AF == null)
            {
                _quadrangle.SetQuadrangle(id);

                var quadrangleTransactionDto = new QuadrangleTransactionDto(itemId, id, applicantId);
                quadrangleTransactionDto.QuadrangleId = id;
                viewModel.QuadrangleTransactionDto = quadrangleTransactionDto;
                viewModel.QuadrangleTransactionDto.Price = _quadrangle.GetPrice();
                viewModel.QuadrangleTransactionDto.Maintenance = _quadrangle.GetMaintenance();
                viewModel.QuadrangleTransactionDto.LifeTimeMaintenance = _quadrangle.GetLifeTimeMaintenance();
            }
            else
            {
                _order.SetTransaction(AF);
                viewModel.QuadrangleTransactionDto = _order.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            if (viewModel.QuadrangleTransactionDto.Deceased1Id == viewModel.QuadrangleTransactionDto.Deceased2Id)
            {
                ModelState.AddModelError("QuadrangleTransactionDto.Deceased1Id", "Same deceased");
                ModelState.AddModelError("QuadrangleTransactionDto.Deceased2Id", "Same deceased");
                return FormForResubmit(viewModel);
            }

            if (viewModel.QuadrangleTransactionDto.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.QuadrangleTransactionDto.Deceased1Id);
                if (_deceased.GetQuadrangle() != null && _deceased.GetQuadrangle().Id != viewModel.QuadrangleTransactionDto.QuadrangleId)
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Deceased1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.QuadrangleTransactionDto.Deceased2Id != null)
            {
                _deceased.SetDeceased((int)viewModel.QuadrangleTransactionDto.Deceased2Id);
                if (_deceased.GetQuadrangle() != null && _deceased.GetQuadrangle().Id != viewModel.QuadrangleTransactionDto.QuadrangleId)
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Deceased2Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.QuadrangleTransactionDto.AF == null)
            {
                if (_order.Create(viewModel.QuadrangleTransactionDto))
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
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.QuadrangleTransactionDto.AF).Any() && 
                    viewModel.QuadrangleTransactionDto.Price + (float)viewModel.QuadrangleTransactionDto.Maintenance + (float)viewModel.QuadrangleTransactionDto.LifeTimeMaintenance <
                _invoice.GetInvoicesByAF(viewModel.QuadrangleTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("QuadrangleTransactionDto.Maintenance", "* Exceed invoice amount");
                    ModelState.AddModelError("QuadrangleTransactionDto.LifeTimeMaintenance", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _order.Update(viewModel.QuadrangleTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.QuadrangleTransactionDto.QuadrangleItemId,
                id = viewModel.QuadrangleTransactionDto.QuadrangleId,
                applicantId = viewModel.QuadrangleTransactionDto.ApplicantId
            });
        }

        public ActionResult FormForResubmit(QuadrangleTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = _funeralCo.GetFuneralCompanyDtos();
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.QuadrangleTransactionDto.ApplicantId);

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _order.SetTransaction(AF);
                _order.Delete();
            }

            return RedirectToAction("Index", new
            {
                itemId,
                id,
                applicantId
            });
        }

        public ActionResult Invoices(string AF)
        {
            return RedirectToAction("Index", "QuadrangleInvoices", new { AF = AF });
        }

    }
}