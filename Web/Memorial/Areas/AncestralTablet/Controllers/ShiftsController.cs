using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.AncestralTablet;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

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

            var ancestralTabletTransactionDtos = Mapper.Map<IEnumerable<AncestralTabletTransactionDto>>(_shift.GetByAncestralTabletIdAndItemId(id, itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage);
            var item = _item.GetById(itemId);
            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestralTabletItemName = item.SubProductService.Name,
                AncestralTabletTransactionDtos = ancestralTabletTransactionDtos,
            };

            var ancestralTablet = _ancestralTablet.GetById(id);
            viewModel.AllowNew = applicantId != null && !ancestralTablet.hasFreeOrder;

            viewModel.AncestralTabletDto = Mapper.Map<AncestralTabletDto>(ancestralTablet);

            viewModel.AncestralTabletId = id;

            return View("ShiftedToIndex", viewModel);

        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _shift.GetByAF(AF);
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
            var viewModel = new AncestralTabletTransactionsFormViewModel();
            viewModel.AncestralTabletDto = Mapper.Map<AncestralTabletDto>(ancestralTablet);

            if (AF == null)
            {
                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto();

                ancestralTabletTransactionDto.ApplicantDtoId = applicantId;

                ancestralTabletTransactionDto.AncestralTabletDto = viewModel.AncestralTabletDto;

                ancestralTabletTransactionDto.AncestralTabletItemDtoId = itemId;

                ancestralTabletTransactionDto.ShiftedAncestralTabletDtoId = id;
                ancestralTabletTransactionDto.ShiftedAncestralTabletDto = viewModel.AncestralTabletDto;

                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
            }
            else
            {
                viewModel.AncestralTabletTransactionDto = Mapper.Map<AncestralTabletTransactionDto>(_shift.GetByAF(AF));

                var shiftedAncestralTablet = _ancestralTablet.GetById((int)viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDtoId);
                viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDto = Mapper.Map<AncestralTabletDto>(shiftedAncestralTablet);
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestralTabletTransactionsFormViewModel viewModel)
        {
            var ancestralTabletTransaction = Mapper.Map<Core.Domain.AncestralTabletTransaction>(viewModel.AncestralTabletTransactionDto);
            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_shift.Add(ancestralTabletTransaction))
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
                    viewModel.AncestralTabletTransactionDto.Price <
                _invoice.GetByAF(viewModel.AncestralTabletTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _shift.Change(ancestralTabletTransaction.AF, ancestralTabletTransaction);
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
            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                var ancestralTablet = _ancestralTablet.GetById(viewModel.AncestralTabletTransactionDto.AncestralTabletDtoId);
                viewModel.AncestralTabletTransactionDto.AncestralTabletDto = Mapper.Map<AncestralTabletDto>(ancestralTablet);

                viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDto = Mapper.Map<AncestralTabletDto>(ancestralTablet);
            }
            else
            {
                viewModel.AncestralTabletTransactionDto = Mapper.Map<AncestralTabletTransactionDto>(_shift.GetByAF(viewModel.AncestralTabletTransactionDto.AF));

                var shiftedAncestralTablet = _ancestralTablet.GetById((int)viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDtoId);
                viewModel.AncestralTabletTransactionDto.ShiftedAncestralTabletDto = Mapper.Map<AncestralTabletDto>(shiftedAncestralTablet);
            }

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _shift.Remove(AF);

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