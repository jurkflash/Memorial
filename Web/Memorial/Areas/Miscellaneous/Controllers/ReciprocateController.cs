using System.Web.Mvc;
using Memorial.Lib.Miscellaneous;
using Memorial.Lib.PlotLandscapeCompany;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Miscellaneous.Controllers
{
    public class ReciprocateController : Controller
    {
        private readonly IMiscellaneous _miscellaneous;
        private readonly IItem _item;
        private readonly IPlotLandscapeCompany _plotLandscapeCompany;
        private readonly IReciprocate _reciprocate;

        public ReciprocateController(
            IMiscellaneous miscellaneous,
            IItem item,
            IPlotLandscapeCompany plotLandscapeCompany,
            IReciprocate reciprocate
            )
        {
            _miscellaneous = miscellaneous;
            _item = item;
            _plotLandscapeCompany = plotLandscapeCompany;
            _reciprocate = reciprocate;
        }

        public ActionResult Index(int itemId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var viewModel = new MiscellaneousItemIndexesViewModel()
            {
                MiscellaneousItemId = itemId,
                MiscellaneousTransactionDtos = _reciprocate.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = true
            };

            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, string AF = null)
        {
            var viewModel = new MiscellaneousTransactionsFormViewModel()
            {
                PlotLandscapeCompanyDtos = _plotLandscapeCompany.GetPlotLandscapeCompanyDtos()
            };

            if (AF == null)
            {
                var miscellanousTransactionDto = new MiscellaneousTransactionDto();
                miscellanousTransactionDto.MiscellaneousItemDtoId = itemId;
                viewModel.MiscellaneousTransactionDto = miscellanousTransactionDto;
                viewModel.MiscellaneousTransactionDto.Amount = _reciprocate.GetItemPrice(itemId);
            }
            else
            {
                _reciprocate.SetTransaction(AF);
                viewModel.MiscellaneousTransactionDto = _reciprocate.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(MiscellaneousTransactionsFormViewModel viewModel)
        {
            if (viewModel.MiscellaneousTransactionDto.AF == null)
            {
                if (!_reciprocate.Create(viewModel.MiscellaneousTransactionDto))
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (!_reciprocate.Update(viewModel.MiscellaneousTransactionDto))
                {
                    ModelState.AddModelError("MiscellanousTransactionDto.Price", "* Exceed receipt amount");

                    return FormForResubmit(viewModel);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.MiscellaneousTransactionDto.MiscellaneousItemDtoId
            });
        }

        public ActionResult FormForResubmit(MiscellaneousTransactionsFormViewModel viewModel)
        {
            viewModel.PlotLandscapeCompanyDtos = _plotLandscapeCompany.GetPlotLandscapeCompanyDtos();

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId)
        {
            _reciprocate.SetTransaction(AF);
            _reciprocate.Delete();

            return RedirectToAction("Index", new
            {
                itemId
            });
        }

        public ActionResult Receipts(string AF)
        {
            return RedirectToAction("Index", "NonOrderReceipts", new { AF = AF, area = "Miscellanous" });
        }

    }
}