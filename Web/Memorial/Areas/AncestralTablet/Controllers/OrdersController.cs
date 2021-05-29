using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.AncestralTablet;
using Memorial.Lib.Deceased;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IDeceased _deceased;
        private readonly IOrder _order;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IAncestralTablet _invoice;

        public OrdersController(
            IAncestralTablet ancestralTablet,
            IArea area,
            IItem item,
            IDeceased deceased,
            IOrder order,
            ITracking tracking,
            Lib.Invoice.IAncestralTablet invoice
            )
        {
            _ancestralTablet = ancestralTablet;
            _area = area;
            _item = item;
            _deceased = deceased;
            _order = order;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _ancestralTablet.SetAncestralTablet(id);
            _item.SetItem(itemId);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestralTabletItemName = _item.GetName(),
                AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto(),
                AncestralTabletId = id,
                AncestralTabletTransactionDtos = _order.GetTransactionDtosByAncestralTabletIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId == 0 || _ancestralTablet.HasApplicant())
            {
                viewModel.AllowNew = false;
            }
            else
            {
                viewModel.AllowNew = true;
            }

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _order.SetTransaction(AF);
            _ancestralTablet.SetAncestralTablet(_order.GetTransactionAncestralTabletId());
            _area.SetArea(_order.GetTransaction().AncestralTabletItem.AncestralTabletAreaId);

            var viewModel = new AncestralTabletTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _order.GetItemName();
            viewModel.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();
            viewModel.AncestralTabletTransactionDto = _order.GetTransactionDto();
            viewModel.ApplicantId = _order.GetTransactionApplicantId();
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
            var viewModel = new AncestralTabletTransactionsFormViewModel()
            {
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            if (AF == null)
            {
                _ancestralTablet.SetAncestralTablet(id);

                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto(itemId, id, applicantId);
                ancestralTabletTransactionDto.AncestralTabletId = id;
                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
                viewModel.AncestralTabletTransactionDto.Price = _ancestralTablet.GetPrice();
                viewModel.AncestralTabletTransactionDto.Maintenance = _ancestralTablet.GetMaintenance();
            }
            else
            {
                _order.SetTransaction(AF);
                viewModel.AncestralTabletTransactionDto = _order.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestralTabletTransactionsFormViewModel viewModel)
        {
            if (viewModel.AncestralTabletTransactionDto.DeceasedId != null)
            {
                _deceased.SetDeceased((int)viewModel.AncestralTabletTransactionDto.DeceasedId);
                if (_deceased.GetAncestralTablet() != null && _deceased.GetAncestralTablet().Id != viewModel.AncestralTabletTransactionDto.AncestralTabletId)
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.DeceasedId", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_order.Create(viewModel.AncestralTabletTransactionDto))
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
                    viewModel.AncestralTabletTransactionDto.Price + (float)viewModel.AncestralTabletTransactionDto.Maintenance <
                _invoice.GetInvoicesByAF(viewModel.AncestralTabletTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("AncestralTabletTransactionDto.Maintenance", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _order.Update(viewModel.AncestralTabletTransactionDto);
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
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.AncestralTabletTransactionDto.ApplicantId);

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _order.SetTransaction(AF);
                _order.Delete();
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
            return RedirectToAction("Index", "AncestralTabletInvoices", new { AF = AF, area = "AncestralTablet" });
        }

    }
}