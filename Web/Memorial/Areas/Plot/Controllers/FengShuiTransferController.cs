using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Plot;
using Memorial.Lib.Applicant;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Plot.Controllers
{
    public class FengShuiTransferController : Controller
    {
        private readonly IPlot _plot;
        private readonly ITransfer _transfer;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IPlot _invoice;

        public FengShuiTransferController(
            IPlot plot,
            IApplicant applicant,
            ITransfer transfer,
            ITracking tracking,
            Lib.Invoice.IPlot invoice
            )
        {
            _plot = plot;
            _applicant = applicant;
            _transfer = transfer;
            _tracking = tracking;
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
                PlotTransactionDtos = _transfer.GetTransactionDtosByPlotIdAndItemId(id, itemId)
            };

            viewModel.AllowNew = applicantId != 0 && _plot.HasApplicant() && _plot.GetApplicantId() != applicantId && _transfer.AllowPlotDeceasePairing(_plot, applicantId);

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _transfer.SetTransaction(AF);
            _plot.SetPlot(_transfer.GetTransactionPlotId());

            var viewModel = new PlotTransactionsInfoViewModel()
            {
                ApplicantId = _transfer.GetTransactionApplicantId(),
                DeceasedId = _transfer.GetTransactionDeceased1Id(),
                PlotDto = _plot.GetPlotDto(),
                ItemName = _transfer.GetItemName(),
                PlotTransactionDto = _transfer.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new PlotTransactionsFormViewModel();

            if (AF == null)
            {
                _plot.SetPlot(id);

                var plotTransactionDto = new PlotTransactionDto(itemId, id, applicantId);
                plotTransactionDto.ApplicantDto = _applicant.GetApplicantDto(applicantId);
                plotTransactionDto.PlotDto = _plot.GetPlotDto();

                viewModel.PlotTransactionDto = plotTransactionDto;
                viewModel.PlotTransactionDto.Price = _transfer.GetItemPrice(itemId);
            }
            else
            {
                _transfer.SetTransaction(AF);
                viewModel.PlotTransactionDto = _transfer.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(PlotTransactionsFormViewModel viewModel)
        {
            if (viewModel.PlotTransactionDto.AF == null)
            {
                _plot.SetPlot(viewModel.PlotTransactionDto.PlotDtoId);

                if (_plot.GetApplicantId() == viewModel.PlotTransactionDto.ApplicantDtoId)
                {
                    ModelState.AddModelError("PlotTransactionDto.Applicant.Name", "Not allow to be same applicant");
                    return FormForResubmit(viewModel);
                }

                if(!_transfer.AllowPlotDeceasePairing(_plot, viewModel.PlotTransactionDto.ApplicantDtoId))
                {
                    ModelState.AddModelError("PlotTransactionDto.Applicant.Name", "Deceased not linked with new applicant");
                    return FormForResubmit(viewModel);
                }

                if (_transfer.Create(viewModel.PlotTransactionDto))
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
                    viewModel.PlotTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.PlotTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("PlotTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }


                _transfer.Update(viewModel.PlotTransactionDto);
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
            viewModel.PlotTransactionDto.ApplicantDto = _applicant.GetApplicantDto(viewModel.PlotTransactionDto.ApplicantDtoId);
            viewModel.PlotTransactionDto.PlotDto = _plot.GetPlotDto(viewModel.PlotTransactionDto.PlotDtoId);

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _transfer.SetTransaction(AF);
                _transfer.Delete();
            }

            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Plot" });
            }
    }
}