using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Cemetery;
using Memorial.Lib.Deceased;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Cemetery.Controllers
{
    public class ClearancesController : Controller
    {
        private readonly IPlot _plot;
        private readonly IDeceased _deceased;
        private readonly IClearance _clearance;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IPlot _invoice;

        public ClearancesController(
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

            var viewModel = new CemeteryItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                CemeteryItemId = itemId,
                PlotDto = _plot.GetPlotDto(),
                PlotId = id,
                CemeteryTransactionDtos = _clearance.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
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

            var viewModel = new CemeteryTransactionsInfoViewModel()
            {
                ApplicantId = _clearance.GetTransactionApplicantId(),
                DeceasedId = _clearance.GetTransactionDeceased1Id(),
                PlotDto = _plot.GetPlotDto(),
                ItemName = _clearance.GetItemName(),
                CemeteryTransactionDto = _clearance.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new CemeteryTransactionsFormViewModel()
            {
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            if (AF == null)
            {
                _plot.SetPlot(id);

                var cemeteryTransactionDto = new CemeteryTransactionDto(itemId, id, applicantId);
                cemeteryTransactionDto.PlotDtoId = id;
                viewModel.CemeteryTransactionDto = cemeteryTransactionDto;
                viewModel.CemeteryTransactionDto.Price = _plot.GetPrice();
            }
            else
            {
                _clearance.SetTransaction(AF);
                viewModel.CemeteryTransactionDto = _clearance.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id == null)
            {
                ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "Please Select");
                return View("Form", viewModel);
            }

            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.CemeteryTransactionDto.DeceasedDto1Id);
                if (_deceased.GetPlot() != null && (_deceased.GetPlot().Id != viewModel.CemeteryTransactionDto.PlotDtoId ||
                    _clearance.GetTransactionsByPlotIdAndDeceased1Id(viewModel.CemeteryTransactionDto.PlotDtoId, (int)viewModel.CemeteryTransactionDto.DeceasedDto1Id).AF != viewModel.CemeteryTransactionDto.AF))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "Invalid");
                    return View("Form", viewModel);
                }
            }

            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                if (_clearance.Create(viewModel.CemeteryTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.CemeteryTransactionDto.CemeteryItemId,
                        id = viewModel.CemeteryTransactionDto.PlotDtoId,
                        applicantId = viewModel.CemeteryTransactionDto.ApplicantDtoId
                    });
                }
                else
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.CemeteryTransactionDto.AF).Any() &&
                    viewModel.CemeteryTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.CemeteryTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Price", "* Exceed invoice amount");

                    return View("Form", viewModel);
                }

                _clearance.Update(viewModel.CemeteryTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.CemeteryTransactionDto.CemeteryItemId,
                id = viewModel.CemeteryTransactionDto.PlotDtoId,
                applicantId = viewModel.CemeteryTransactionDto.ApplicantDtoId
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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Cemetery" });
        }
    }
}