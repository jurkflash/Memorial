using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Ancestor;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly IAncestor _ancestor;
        private readonly IShift _shift;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IAncestor _invoice;

        public ShiftsController(
            IAncestor ancestor,
            IShift shift,
            ITracking tracking,
            Lib.Invoice.IAncestor invoice
            )
        {
            _ancestor = ancestor;
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

            var ancestralTabletTransactionDtos = _shift.GetTransactionDtosByAncestorIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestralTabletTransactionDtos = ancestralTabletTransactionDtos,
            };

            _ancestor.SetAncestor(id);

            viewModel.AllowNew = applicantId != 0 && !_ancestor.HasFreeOrder();

            viewModel.AncestorDto = _ancestor.GetAncestorDto();

            viewModel.AncestorId = id;

            return View("ShiftedToIndex", viewModel);

        }

        public ActionResult Info(string AF)
        {
            _shift.SetTransaction(AF);
            _ancestor.SetAncestor(_shift.GetTransactionAncestorId());

            var viewModel = new AncestralTabletTransactionsInfoViewModel()
            {
                ApplicantId = _shift.GetTransactionApplicantId(),
                DeceasedId = _shift.GetTransactionDeceasedId(),
                AncestorDto = _ancestor.GetAncestorDto(),
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
                _ancestor.SetAncestor(id);

                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto();

                ancestralTabletTransactionDto.ApplicantId = applicantId;

                ancestralTabletTransactionDto.AncestralTabletItemId = itemId;

                ancestralTabletTransactionDto.ShiftedAncestorId = id;
                ancestralTabletTransactionDto.ShiftedAncestor = _ancestor.GetAncestor();

                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
            }
            else
            {
                viewModel.AncestralTabletTransactionDto = _shift.GetTransactionDto(AF);

                _ancestor.SetAncestor((int)viewModel.AncestralTabletTransactionDto.ShiftedAncestorId);

                viewModel.AncestralTabletTransactionDto.ShiftedAncestor = _ancestor.GetAncestor();
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
                        id = viewModel.AncestralTabletTransactionDto.AncestorId,
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
                id = viewModel.AncestralTabletTransactionDto.AncestorId,
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
            return RedirectToAction("Index", "AncestorInvoices", new { AF = AF, area = "AncestralTablet" });
        }
    }
}