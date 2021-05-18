using System.Web.Mvc;
using Memorial.Lib.Plot;
using Memorial.Lib.FengShuiMaster;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Plot.Controllers
{
    public class ReciprocatesController : Controller
    {
        private readonly IPlot _plot;
        private readonly IFengShuiMaster _fengShuiMaster;
        private readonly IReciprocate _reciprocate;

        public ReciprocatesController(
            IPlot plot,
            IFengShuiMaster fengShuiMaster,
            IReciprocate reciprocate
            )
        {
            _plot = plot;
            _fengShuiMaster = fengShuiMaster;
            _reciprocate = reciprocate;
        }

        public ActionResult Index(int itemId, int id, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _plot.SetPlot(id);

            var viewModel = new PlotItemIndexesViewModel()
            {
                PlotItemId = itemId,
                PlotDto = _plot.GetPlotDto(),
                PlotId = id,
                PlotTransactionDtos = _reciprocate.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (!_plot.HasApplicant() || _plot.HasCleared())
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
            _reciprocate.SetTransaction(AF);
            _plot.SetPlot(_reciprocate.GetTransactionPlotId());

            var viewModel = new PlotTransactionsInfoViewModel()
            {
                ApplicantId = _reciprocate.GetTransactionApplicantId(),
                DeceasedId = _reciprocate.GetTransactionDeceased1Id(),
                PlotDto = _plot.GetPlotDto(),
                ItemName = _reciprocate.GetItemName(),
                PlotTransactionDto = _reciprocate.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, string AF = null)
        {
            var viewModel = new PlotTransactionsFormViewModel()
            {
                FengShuiMasterDtos = _fengShuiMaster.GetFengShuiMasterDtos()
            };

            if (AF == null)
            {
                _plot.SetPlot(id);

                var plotTransactionDto = new PlotTransactionDto();
                plotTransactionDto.PlotDtoId = id;
                plotTransactionDto.PlotItemId = itemId;
                viewModel.PlotTransactionDto = plotTransactionDto;
                viewModel.PlotTransactionDto.Price = _reciprocate.GetItemPrice();
            }
            else
            {
                _reciprocate.SetTransaction(AF);
                viewModel.PlotTransactionDto = _reciprocate.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(PlotTransactionsFormViewModel viewModel)
        {
            if (viewModel.PlotTransactionDto.AF == null)
            {
                if (_reciprocate.Create(viewModel.PlotTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.PlotTransactionDto.PlotItemId,
                        id = viewModel.PlotTransactionDto.PlotDtoId
                    });
                }
                else
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (!_reciprocate.Update(viewModel.PlotTransactionDto))
                {
                    ModelState.AddModelError("PlotTransactionDto.Price", "* Exceed receipt amount");

                    return FormForResubmit(viewModel);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.PlotTransactionDto.PlotItemId,
                id = viewModel.PlotTransactionDto.PlotDtoId
            });
        }

        public ActionResult FormForResubmit(PlotTransactionsFormViewModel viewModel)
        {
            viewModel.FengShuiMasterDtos = _fengShuiMaster.GetFengShuiMasterDtos();

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id)
        {
            _reciprocate.SetTransaction(AF);
            _reciprocate.Delete();

            return RedirectToAction("Index", new
            {
                itemId,
                id
            });
        }

        public ActionResult Receipts(string AF)
        {
            return RedirectToAction("Index", "NonOrderReceipts", new { AF = AF, area = "Plot" });
        }

    }
}