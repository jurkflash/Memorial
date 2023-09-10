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
    public class MaintenancesController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IMaintenance _maintenance;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly Lib.Invoice.IAncestralTablet _invoice;

        public MaintenancesController(
            IAncestralTablet ancestralTablet,
            IMaintenance maintenance,
            IArea area,
            IItem item,
            Lib.Invoice.IAncestralTablet invoice
            )
        {
            _ancestralTablet = ancestralTablet;
            _maintenance = maintenance;
            _area = area;
            _item = item;
            _invoice = invoice;
        }

        [HttpGet]
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
                AncestralTabletTransactionDtos = Mapper.Map<IEnumerable<AncestralTabletTransactionDto>>(_maintenance.GetByAncestralTabletIdAndItemId(id, itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null && ancestralTablet.ApplicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _maintenance.GetByAF(AF);
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
            var item = _item.GetById(itemId);
            var viewModel = new AncestralTabletTransactionsFormViewModel();
            viewModel.AncestralTabletDto = Mapper.Map<AncestralTabletDto>(ancestralTablet);

            if (AF == null)
            {
                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto(itemId, id, applicantId);
                ancestralTabletTransactionDto.AncestralTabletDtoId = id;
                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
                viewModel.AncestralTabletTransactionDto.Price = _item.GetPrice(item);
            }
            else
            {
                viewModel.AncestralTabletTransactionDto = Mapper.Map<AncestralTabletTransactionDto>(_maintenance.GetByAF(AF));
            }

            return View(viewModel);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Save(AncestralTabletTransactionsFormViewModel viewModel)
        {
            var ancestralTabletTransaction = Mapper.Map<Core.Domain.AncestralTabletTransaction>(viewModel.AncestralTabletTransactionDto);
            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_maintenance.Add(ancestralTabletTransaction))
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
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.GetByAF(viewModel.AncestralTabletTransactionDto.AF).Any() &&
                    viewModel.AncestralTabletTransactionDto.Price <
                _invoice.GetByAF(viewModel.AncestralTabletTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.Price", "* Exceed invoice amount");
                    return View("Form", viewModel);
                }


                _maintenance.Change(ancestralTabletTransaction.AF, ancestralTabletTransaction);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemDtoId,
                id = viewModel.AncestralTabletTransactionDto.AncestralTabletDtoId,
                applicantId = viewModel.AncestralTabletTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _maintenance.Remove(AF);

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