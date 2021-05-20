using System.Web.Mvc;
using Memorial.Lib.Cemetery;
using Memorial.Lib.FengShuiMaster;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Cemetery.Controllers
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

            var viewModel = new CemeteryItemIndexesViewModel()
            {
                CemeteryItemId = itemId,
                PlotDto = _plot.GetPlotDto(),
                PlotId = id,
                CemeteryTransactionDtos = _reciprocate.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
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

            var viewModel = new CemeteryTransactionsInfoViewModel()
            {
                ApplicantId = _reciprocate.GetTransactionApplicantId(),
                DeceasedId = _reciprocate.GetTransactionDeceased1Id(),
                PlotDto = _plot.GetPlotDto(),
                ItemName = _reciprocate.GetItemName(),
                CemeteryTransactionDto = _reciprocate.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, string AF = null)
        {
            var viewModel = new CemeteryTransactionsFormViewModel()
            {
                FengShuiMasterDtos = _fengShuiMaster.GetFengShuiMasterDtos()
            };

            if (AF == null)
            {
                _plot.SetPlot(id);

                var cemeteryTransactionDto = new CemeteryTransactionDto();
                cemeteryTransactionDto.PlotDtoId = id;
                cemeteryTransactionDto.CemeteryItemId = itemId;
                viewModel.CemeteryTransactionDto = cemeteryTransactionDto;
                viewModel.CemeteryTransactionDto.Price = _reciprocate.GetItemPrice();
            }
            else
            {
                _reciprocate.SetTransaction(AF);
                viewModel.CemeteryTransactionDto = _reciprocate.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                if (_reciprocate.Create(viewModel.CemeteryTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.CemeteryTransactionDto.CemeteryItemId,
                        id = viewModel.CemeteryTransactionDto.PlotDtoId
                    });
                }
                else
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (!_reciprocate.Update(viewModel.CemeteryTransactionDto))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Price", "* Exceed receipt amount");

                    return FormForResubmit(viewModel);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.CemeteryTransactionDto.CemeteryItemId,
                id = viewModel.CemeteryTransactionDto.PlotDtoId
            });
        }

        public ActionResult FormForResubmit(CemeteryTransactionsFormViewModel viewModel)
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