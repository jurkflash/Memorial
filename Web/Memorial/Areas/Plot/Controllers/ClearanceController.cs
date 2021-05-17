using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Plot;
using Memorial.Lib.Deceased;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Plot.Controllers
{
    public class ClearanceController : Controller
    {
        private readonly IPlot _plot;
        private readonly IDeceased _deceased;
        private readonly IClearance _clearance;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IPlot _invoice;

        public ClearanceController(
            IPlot plot,
            IDeceased deceased,
            IClearance clearance,
            ITracking tracking,
            Lib.Invoice.IPlot invoice
            )
        {
            _plot = plot;
            _deceased = deceased;
            _clearance = clearance;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _plot.SetPlot(id);

            var viewModel = new PlotItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                PlotItemId = itemId,
                PlotDto = _plot.GetPlotDto(),
                PlotId = id,
                PlotTransactionDtos = _clearance.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId != 0 && _plot.HasApplicant() && _deceased.GetDeceasedsByPlotId(id).Count() > 0 && !_plot.HasCleared())
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
            _clearance.SetTransaction(AF);
            _plot.SetPlot(_clearance.GetTransactionPlotId());

            var viewModel = new PlotTransactionsInfoViewModel()
            {
                ApplicantId = _clearance.GetTransactionApplicantId(),
                DeceasedId = _clearance.GetTransactionDeceased1Id(),
                PlotDto = _plot.GetPlotDto(),
                ItemName = _clearance.GetItemName(),
                PlotTransactionDto = _clearance.GetTransactionDto()
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
                _clearance.SetTransaction(AF);
                viewModel.PlotTransactionDto = _clearance.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(PlotTransactionsFormViewModel viewModel)
        {
            if (viewModel.PlotTransactionDto.DeceasedDto1Id == null)
            {
                ModelState.AddModelError("PlotTransactionDto.DeceasedDto1Id", "Please Select");
                return View("Form", viewModel);
            }

            if (viewModel.PlotTransactionDto.DeceasedDto1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.PlotTransactionDto.DeceasedDto1Id);
                if (_deceased.GetPlot() != null && (_deceased.GetPlot().Id != viewModel.PlotTransactionDto.PlotDtoId ||
                    _clearance.GetTransactionsByPlotIdAndDeceased1Id(viewModel.PlotTransactionDto.PlotDtoId, (int)viewModel.PlotTransactionDto.DeceasedDto1Id).AF != viewModel.PlotTransactionDto.AF))
                {
                    ModelState.AddModelError("PlotTransactionDto.DeceasedDto1Id", "Invalid");
                    return View("Form", viewModel);
                }
            }

            if (viewModel.PlotTransactionDto.AF == null)
            {
                if (_clearance.Create(viewModel.PlotTransactionDto))
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
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.PlotTransactionDto.AF).Any() &&
                    viewModel.PlotTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.PlotTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("PlotTransactionDto.Price", "* Exceed invoice amount");

                    return View("Form", viewModel);
                }

                _clearance.Update(viewModel.PlotTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.PlotTransactionDto.PlotItemId,
                id = viewModel.PlotTransactionDto.PlotDtoId,
                applicantId = viewModel.PlotTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _clearance.SetTransaction(AF);
                _clearance.Delete();
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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Plot" });
        }
    }
}