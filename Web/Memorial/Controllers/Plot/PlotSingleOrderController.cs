using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Plot;
using Memorial.Lib.Deceased;
using Memorial.Lib.Applicant;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Controllers
{
    public class PlotSingleOrderController : Controller
    {
        private readonly IPlot _plot;
        private readonly IDeceased _deceased;
        private readonly ISingleOrder _order;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly IPlotApplicantDeceaseds _plotApplicantDeceaseds;
        private readonly Lib.Invoice.IPlot _invoice;

        public PlotSingleOrderController(
            IPlot plot,
            IApplicant applicant,
            IDeceased deceased,
            ISingleOrder order,
            ITracking tracking,
            IPlotApplicantDeceaseds plotApplicantDeceaseds,
            Lib.Invoice.IPlot invoice
            )
        {
            _plot = plot;
            _applicant = applicant;
            _deceased = deceased;
            _order = order;
            _tracking = tracking;
            _plotApplicantDeceaseds = plotApplicantDeceaseds;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId)
        {
            _plot.SetPlot(id);

            var viewModel = new PlotItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                PlotItemId = itemId,
                PlotDto = _plot.GetPlotDto(),
                PlotId = id,
                PlotTransactionDtos = _order.GetTransactionDtosByPlotIdAndItemId(id, itemId),
            };

            if (_plot.HasApplicant())
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
            _plot.SetPlot(_order.GetTransactionPlotId());

            var viewModel = new PlotTransactionsInfoViewModel()
            {
                ApplicantId = _order.GetTransactionApplicantId(),
                DeceasedId = _order.GetTransactionDeceased1Id(),
                PlotDto = _plot.GetPlotDto(),
                ItemName = _order.GetItemName(),
                PlotTransactionDto = _order.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new PlotTransactionsFormViewModel()
            {
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            if (AF == null)
            {
                _plot.SetPlot(id);

                var plotTransactionDto = new PlotTransactionDto(itemId, id, applicantId);
                plotTransactionDto.PlotDtoId = id;
                viewModel.PlotTransactionDto = plotTransactionDto;
                viewModel.PlotTransactionDto.Price = _plot.GetPrice();
                viewModel.PlotTransactionDto.Maintenance = _plot.GetMaintenance();
                viewModel.PlotTransactionDto.Brick = _plot.GetBrick();
                viewModel.PlotTransactionDto.Dig = _plot.GetDig();
                viewModel.PlotTransactionDto.Wall = _plot.GetWall();
            }
            else
            {
                _order.SetTransaction(AF);
                viewModel.PlotTransactionDto = _order.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(PlotTransactionsFormViewModel viewModel)
        {
            if (viewModel.PlotTransactionDto.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.PlotTransactionDto.Deceased1Id);
                if (_deceased.GetPlot() != null && _deceased.GetPlot().Id != viewModel.PlotTransactionDto.PlotDtoId)
                {
                    ModelState.AddModelError("PlotTransactionDto.Deceased1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.PlotTransactionDto.AF == null)
            {
                if (_order.Create(viewModel.PlotTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.PlotTransactionDto.PlotItemId,
                        id = viewModel.PlotTransactionDto.PlotDtoId,
                        applicantId = viewModel.PlotTransactionDto.ApplicantDtoId
                    });
                }
                else
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.PlotTransactionDto.AF).Any() &&
                    viewModel.PlotTransactionDto.Price + 
                    (float)viewModel.PlotTransactionDto.Maintenance + 
                    (float)viewModel.PlotTransactionDto.Brick + 
                    (float)viewModel.PlotTransactionDto.Dig + 
                    (float)viewModel.PlotTransactionDto.Wall
                    <
                _invoice.GetInvoicesByAF(viewModel.PlotTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("PlotTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("PlotTransactionDto.Maintenance", "* Exceed invoice amount");
                    ModelState.AddModelError("PlotTransactionDto.Brick", "* Exceed invoice amount");
                    ModelState.AddModelError("PlotTransactionDto.Dig", "* Exceed invoice amount");
                    ModelState.AddModelError("PlotTransactionDto.Wall", "* Exceed invoice amount");

                    return FormForResubmit(viewModel);
                }

                _order.Update(viewModel.PlotTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.PlotTransactionDto.PlotItemId,
                id = viewModel.PlotTransactionDto.PlotDtoId,
                applicantId = viewModel.PlotTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult FormForResubmit(PlotTransactionsFormViewModel viewModel)
        {
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.PlotTransactionDto.ApplicantDtoId);

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
            return RedirectToAction("Index", "PlotInvoices", new { AF = AF });
        }

    }
}