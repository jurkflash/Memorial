using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.AncestralTablet;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class MaintenancesController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IMaintenance _maintenance;
        private readonly Lib.Invoice.IAncestralTablet _invoice;

        public MaintenancesController(
            IAncestralTablet ancestralTablet,
            IMaintenance maintenance,
            Lib.Invoice.IAncestralTablet invoice
            )
        {
            _ancestralTablet = ancestralTablet;
            _maintenance = maintenance;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _ancestralTablet.SetAncestralTablet(id);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto(),
                AncestralTabletId = id,
                AncestralTabletTransactionDtos = _maintenance.GetTransactionDtosByAncestralTabletIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != 0 && _ancestralTablet.HasApplicant()
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _maintenance.SetTransaction(AF);
            _ancestralTablet.SetAncestralTablet(_maintenance.GetTransactionAncestralTabletId());

            var viewModel = new AncestralTabletTransactionsInfoViewModel()
            {
                ApplicantId = _maintenance.GetTransactionApplicantId(),
                AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto(),
                DeceasedId = _maintenance.GetTransactionDeceasedId(),
                ItemName = _maintenance.GetItemName(),
                AncestralTabletTransactionDto = _maintenance.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new AncestralTabletTransactionsFormViewModel();

            if (AF == null)
            {
                _ancestralTablet.SetAncestralTablet(id);

                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto(itemId, id, applicantId);
                ancestralTabletTransactionDto.AncestralTabletId = id;
                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
                viewModel.AncestralTabletTransactionDto.Price = _maintenance.GetPrice(itemId);
            }
            else
            {
                _maintenance.SetTransaction(AF);
                viewModel.AncestralTabletTransactionDto = _maintenance.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestralTabletTransactionsFormViewModel viewModel)
        {
            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_maintenance.Create(viewModel.AncestralTabletTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemId,
                        id = viewModel.AncestralTabletTransactionDto.AncestralTabletId,
                        applicantId = viewModel.AncestralTabletTransactionDto.ApplicantId
                    });
                }
                else
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.AncestralTabletTransactionDto.AF).Any() &&
                    viewModel.AncestralTabletTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.AncestralTabletTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.Price", "* Exceed invoice amount");
                    return View("Form", viewModel);
                }


                _maintenance.Update(viewModel.AncestralTabletTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemId,
                id = viewModel.AncestralTabletTransactionDto.AncestralTabletId,
                applicantId = viewModel.AncestralTabletTransactionDto.ApplicantId
            });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _maintenance.SetTransaction(AF);
            _maintenance.Delete();

            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "AncestralTabletInvoices", new { AF = AF, area = "AncestralTablet" });
        }
    }
}