using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.AncestralTablet;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IShift _shift;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IAncestralTablet _invoice;

        public ShiftsController(
            IAncestralTablet ancestralTablet,
            IShift shift,
            ITracking tracking,
            Lib.Invoice.IAncestralTablet invoice
            )
        {
            _ancestralTablet = ancestralTablet;
            _shift = shift;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var ancestralTabletTransactionDtos = _shift.GetTransactionDtosByAncestralTabletIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestralTabletTransactionDtos = ancestralTabletTransactionDtos,
            };

            _ancestralTablet.SetAncestralTablet(id);

            viewModel.AllowNew = applicantId != 0 && !_ancestralTablet.HasFreeOrder();

            viewModel.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();

            viewModel.AncestralTabletId = id;

            return View("ShiftedToIndex", viewModel);

        }

        public ActionResult Info(string AF)
        {
            _shift.SetTransaction(AF);
            _ancestralTablet.SetAncestralTablet(_shift.GetTransactionAncestralTabletId());

            var viewModel = new AncestralTabletTransactionsInfoViewModel()
            {
                ApplicantId = _shift.GetTransactionApplicantId(),
                DeceasedId = _shift.GetTransactionDeceasedId(),
                AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto(),
                ItemName = _shift.GetItemName(),
                AncestralTabletTransactionDto = _shift.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new AncestralTabletTransactionsFormViewModel();

            if (AF == null)
            {
                _ancestralTablet.SetAncestralTablet(id);

                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto();

                ancestralTabletTransactionDto.ApplicantId = applicantId;

                ancestralTabletTransactionDto.AncestralTabletItemId = itemId;

                ancestralTabletTransactionDto.ShiftedAncestralTabletId = id;
                ancestralTabletTransactionDto.ShiftedAncestralTablet = _ancestralTablet.GetAncestralTablet();

                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
            }
            else
            {
                viewModel.AncestralTabletTransactionDto = _shift.GetTransactionDto(AF);

                _ancestralTablet.SetAncestralTablet((int)viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletId);

                viewModel.AncestralTabletTransactionDto.ShiftedAncestralTablet = _ancestralTablet.GetAncestralTablet();
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestralTabletTransactionsFormViewModel viewModel)
        {
            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_shift.Create(viewModel.AncestralTabletTransactionDto))
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
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.AncestralTabletTransactionDto.AF).Any() &&
                    viewModel.AncestralTabletTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.AncestralTabletTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _shift.Update(viewModel.AncestralTabletTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemId,
                id = viewModel.AncestralTabletTransactionDto.AncestralTabletId,
                applicantId = viewModel.AncestralTabletTransactionDto.ApplicantId
            });
        }

        public ActionResult FormForResubmit(AncestralTabletTransactionsFormViewModel viewModel)
        {
            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _shift.SetTransaction(AF);
            _shift.Delete();

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