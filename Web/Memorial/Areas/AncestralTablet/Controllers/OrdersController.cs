using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.AncestralTablet;
using Memorial.Lib.Deceased;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

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

        public ActionResult Index(int itemId, int id, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var ancestralTablet = _ancestralTablet.GetById(id);
            var item = _item.GetById(itemId);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestralTabletItemName = item.SubProductService.Name,
                AncestralTabletDto = Mapper.Map<AncestralTabletDto>(ancestralTablet),
                AncestralTabletId = id,
                AncestralTabletTransactionDtos = Mapper.Map<IEnumerable<AncestralTabletTransactionDto>>(_order.GetByAncestralTabletIdAndItemId(id, itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId == null || ancestralTablet.ApplicantId != null)
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
            var transaction = _order.GetByAF(AF);
            var item = _item.GetById(transaction.AncestralTabletItemId);
            var area = _area.GetById(transaction.AncestralTabletItem.AncestralTabletAreaId);
            var viewModel = new AncestralTabletTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = item.SubProductService.Name;
            viewModel.AncestralTabletDto = Mapper.Map<AncestralTabletDto>(transaction.AncestralTablet);
            viewModel.AncestralTabletTransactionDto = Mapper.Map<AncestralTabletTransactionDto>(transaction);
            viewModel.ApplicantId = transaction.ApplicantId;
            viewModel.Header = area.Site.Header;

            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            report.Cookies = cookieCollection;

            return report;
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var ancestralTablet = _ancestralTablet.GetById(id);

            var viewModel = new AncestralTabletTransactionsFormViewModel()
            {
                DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(applicantId)),
                AncestralTabletDto = Mapper.Map<AncestralTabletDto>(ancestralTablet)
            };

            var item = _item.GetById(itemId);
            if (AF == null)
            {
                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto(itemId, id, applicantId);
                ancestralTabletTransactionDto.AncestralTabletDtoId = id;
                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
                viewModel.AncestralTabletTransactionDto.Price = _item.GetPrice(item);
                viewModel.AncestralTabletTransactionDto.Maintenance = ancestralTablet.Maintenance;
            }
            else
            {
                viewModel.AncestralTabletTransactionDto = Mapper.Map<AncestralTabletTransactionDto>(_order.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestralTabletTransactionsFormViewModel viewModel)
        {
            var ancestralTabletTransaction = Mapper.Map<Core.Domain.AncestralTabletTransaction>(viewModel.AncestralTabletTransactionDto);
            if (viewModel.AncestralTabletTransactionDto.DeceasedDtoId != null)
            {
                var deceased = _deceased.GetById((int)viewModel.AncestralTabletTransactionDto.DeceasedDtoId);
                if (deceased.AncestralTabletId != null && deceased.AncestralTabletId != viewModel.AncestralTabletTransactionDto.AncestralTabletDtoId)
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.DeceasedDtoId", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_order.Add(ancestralTabletTransaction))
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
                if (_invoice.GetByAF(viewModel.AncestralTabletTransactionDto.AF).Any() &&
                    viewModel.AncestralTabletTransactionDto.Price + (float)viewModel.AncestralTabletTransactionDto.Maintenance <
                _invoice.GetByAF(viewModel.AncestralTabletTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("AncestralTabletTransactionDto.Maintenance", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _order.Change(ancestralTabletTransaction.AF, ancestralTabletTransaction);
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
            viewModel.DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(viewModel.AncestralTabletTransactionDto.ApplicantDtoId));

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _order.Remove(AF);
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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "AncestralTablet" });
        }

    }
}