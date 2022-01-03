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
    public class FreeController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IDeceased _deceased;
        private readonly IOrder _order;
        private readonly ITracking _tracking;

        public FreeController(
            IAncestralTablet ancestralTablet,
            IArea area,
            IItem item,
            IDeceased deceased,
            IOrder order,
            ITracking tracking
            )
        {
            _ancestralTablet = ancestralTablet;
            _area = area;
            _item = item;
            _deceased = deceased;
            _order = order;
            _tracking = tracking;
        }

        public ActionResult Index(int itemId, int id, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _ancestralTablet.SetAncestralTablet(id);
            _item.SetItem(itemId);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestralTabletItemName = _item.GetName(),
                AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto(),
                AncestralTabletId = id,
                AncestralTabletTransactionDtos = _order.GetTransactionDtosByAncestralTabletIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId == null || _ancestralTablet.HasApplicant())
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
                ancestralTabletTransactionDto.AncestralTabletDtoId = id;
                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
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
            if (viewModel.AncestralTabletTransactionDto.DeceasedDtoId != null)
            {
                _deceased.SetDeceased((int)viewModel.AncestralTabletTransactionDto.DeceasedDtoId);
                if (_deceased.GetAncestralTablet() != null && _deceased.GetAncestralTablet().Id != viewModel.AncestralTabletTransactionDto.AncestralTabletDtoId)
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.DeceasedDtoId", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_order.Create(viewModel.AncestralTabletTransactionDto))
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
                _order.Update(viewModel.AncestralTabletTransactionDto);
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
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.AncestralTabletTransactionDto.ApplicantDtoId);

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

    }
}