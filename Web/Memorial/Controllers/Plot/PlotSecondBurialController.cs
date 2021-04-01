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
    public class PlotSecondBurialController : Controller
    {
        private readonly IPlot _plot;
        private readonly IDeceased _deceased;
        private readonly ISecondBurial _secondBurial;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly IPlotApplicantDeceaseds _plotApplicantDeceaseds;
        private readonly Lib.Invoice.IPlot _invoice;

        public PlotSecondBurialController(
            IPlot plot,
            IApplicant applicant,
            IDeceased deceased,
            ISecondBurial secondBurial,
            ITracking tracking,
            IPlotApplicantDeceaseds plotApplicantDeceaseds,
            Lib.Invoice.IPlot invoice
            )
        {
            _plot = plot;
            _applicant = applicant;
            _deceased = deceased;
            _secondBurial = secondBurial;
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
                PlotTransactionDtos = _secondBurial.GetTransactionDtosByPlotIdAndItemId(id, itemId),
            };

            if (applicantId != 0 && _plot.HasApplicant() && _deceased.GetDeceasedsByPlotId(id).Count() == 1)
            {
                viewModel.AllowNew = true;
            }
            else
            {
                viewModel.AllowNew = false;
            }

            return View("Index", viewModel);
        }

        public ActionResult Info(string AF)
        {
            _secondBurial.SetTransaction(AF);
            _plot.SetPlot(_secondBurial.GetTransactionPlotId());

            var viewModel = new PlotTransactionsInfoViewModel()
            {
                ApplicantId = _secondBurial.GetTransactionApplicantId(),
                DeceasedId = _secondBurial.GetTransactionDeceased1Id(),
                PlotDto = _plot.GetPlotDto(),
                ItemName = _secondBurial.GetItemName(),
                PlotTransactionDto = _secondBurial.GetTransactionDto()
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
            }
            else
            {
                _secondBurial.SetTransaction(AF);
                viewModel.PlotTransactionDto = _secondBurial.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(PlotTransactionsFormViewModel viewModel)
        {
            if (viewModel.PlotTransactionDto.DeceasedDto1Id == null)
            {
                ModelState.AddModelError("PlotTransactionDto.DeceasedDto1Id", "Please Select");
                return FormForResubmit(viewModel);
            }

            if (viewModel.PlotTransactionDto.DeceasedDto1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.PlotTransactionDto.DeceasedDto1Id);
                if (_deceased.GetPlot() != null && (_deceased.GetPlot().Id != viewModel.PlotTransactionDto.PlotDtoId ||
                    _secondBurial.GetTransactionsByPlotIdAndDeceased1Id(viewModel.PlotTransactionDto.PlotDtoId, (int)viewModel.PlotTransactionDto.DeceasedDto1Id).AF != viewModel.PlotTransactionDto.AF))
                {
                    ModelState.AddModelError("PlotTransactionDto.DeceasedDto1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.PlotTransactionDto.AF == null)
            {
                if (_secondBurial.Create(viewModel.PlotTransactionDto))
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

                _secondBurial.Update(viewModel.PlotTransactionDto);
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
                _secondBurial.SetTransaction(AF);
                _secondBurial.Delete();
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