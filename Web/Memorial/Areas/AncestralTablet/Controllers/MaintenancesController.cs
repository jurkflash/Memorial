using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Ancestor;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class MaintenancesController : Controller
    {
        private readonly IAncestor _ancestor;
        private readonly IMaintenance _maintenance;
        private readonly Lib.Invoice.IAncestor _invoice;

        public MaintenancesController(
            IAncestor ancestor,
            IMaintenance maintenance,
            Lib.Invoice.IAncestor invoice
            )
        {
            _ancestor = ancestor;
            _maintenance = maintenance;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _ancestor.SetAncestor(id);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestorDto = _ancestor.GetAncestorDto(),
                AncestorId = id,
                AncestralTabletTransactionDtos = _maintenance.GetTransactionDtosByAncestorIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != 0 && _ancestor.HasApplicant()
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _maintenance.SetTransaction(AF);
            _ancestor.SetAncestor(_maintenance.GetTransactionAncestorId());

            var viewModel = new AncestralTabletTransactionsInfoViewModel()
            {
                ApplicantId = _maintenance.GetTransactionApplicantId(),
                AncestorDto = _ancestor.GetAncestorDto(),
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
                _ancestor.SetAncestor(id);

                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto(itemId, id, applicantId);
                ancestralTabletTransactionDto.AncestorId = id;
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
                        id = viewModel.AncestralTabletTransactionDto.AncestorId,
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
                id = viewModel.AncestralTabletTransactionDto.AncestorId,
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
            return RedirectToAction("Index", "AncestorInvoices", new { AF = AF, area = "AncestralTablet" });
        }
    }
}