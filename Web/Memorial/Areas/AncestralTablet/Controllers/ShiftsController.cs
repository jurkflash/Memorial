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
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IAncestralTablet _invoice;

        public ShiftsController(
            IAncestralTablet ancestralTablet,
            IShift shift,
            IArea area,
            IItem item,
            ITracking tracking,
            Lib.Invoice.IAncestralTablet invoice
            )
        {
            _ancestralTablet = ancestralTablet;
            _shift = shift;
            _area = area;
            _item = item;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var ancestralTabletTransactionDtos = _shift.GetTransactionDtosByAncestralTabletIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage);
            _item.SetItem(itemId);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestralTabletItemName = _item.GetName(),
                AncestralTabletTransactionDtos = ancestralTabletTransactionDtos,
            };

            _ancestralTablet.SetAncestralTablet(id);

            viewModel.AllowNew = applicantId != null && !_ancestralTablet.HasFreeOrder();

            viewModel.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();

            viewModel.AncestralTabletId = id;

            return View("ShiftedToIndex", viewModel);

        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _shift.SetTransaction(AF);
            _ancestralTablet.SetAncestralTablet(_shift.GetTransactionAncestralTabletId());
            _area.SetArea(_shift.GetTransaction().AncestralTabletItem.AncestralTabletAreaId);

            var viewModel = new AncestralTabletTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _shift.GetItemName();
            viewModel.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();
            viewModel.AncestralTabletTransactionDto = _shift.GetTransactionDto();
            viewModel.ApplicantId = _shift.GetTransactionApplicantId();
            viewModel.Header = _area.GetArea().Site.Header;

            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            return report;
        }


        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            _ancestralTablet.SetAncestralTablet(id);

            var viewModel = new AncestralTabletTransactionsFormViewModel();
            viewModel.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();

            if (AF == null)
            {
                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto();

                ancestralTabletTransactionDto.ApplicantDtoId = applicantId;

                ancestralTabletTransactionDto.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();

                ancestralTabletTransactionDto.AncestralTabletItemDtoId = itemId;

                ancestralTabletTransactionDto.ShiftedAncestralTabletDtoId = id;
                ancestralTabletTransactionDto.ShiftedAncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();

                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
            }
            else
            {
                viewModel.AncestralTabletTransactionDto = _shift.GetTransactionDto(AF);

                _ancestralTablet.SetAncestralTablet((int)viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDtoId);

                viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();
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
                        itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemDtoId,
                        id = viewModel.AncestralTabletTransactionDto.AncestralTabletDtoId,
                        applicantId = viewModel.AncestralTabletTransactionDto.ApplicantDtoId
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
                itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemDtoId,
                id = viewModel.AncestralTabletTransactionDto.AncestralTabletDtoId,
                applicantId = viewModel.AncestralTabletTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult FormForResubmit(AncestralTabletTransactionsFormViewModel viewModel)
        {
            viewModel.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();

            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                viewModel.AncestralTabletTransactionDto.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();

                viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();
            }
            else
            {
                viewModel.AncestralTabletTransactionDto = _shift.GetTransactionDto(viewModel.AncestralTabletTransactionDto.AF);

                _ancestralTablet.SetAncestralTablet((int)viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDtoId);

                viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();
            }

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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "AncestralTablet" });
        }
    }
}