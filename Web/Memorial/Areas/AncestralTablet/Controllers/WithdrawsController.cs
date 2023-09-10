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
    public class WithdrawsController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IWithdraw _withdraw;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly Lib.Invoice.IAncestralTablet _invoice;

        public WithdrawsController(
            IAncestralTablet ancestralTablet,
            IWithdraw withdraw,
            IArea area,
            IItem item,
            Lib.Invoice.IAncestralTablet invoice
            )
        {
            _ancestralTablet = ancestralTablet;
            _withdraw = withdraw;
            _area = area;
            _item = item;
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
                AncestralTabletTransactionDtos = Mapper.Map<IEnumerable<AncestralTabletTransactionDto>>(_withdraw.GetByAncestralTabletIdAndItemId(id, itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            if(applicantId != null && !_withdraw.GetByAncestralTabletIdAndItemId(id, itemId, null).Any())
            {
                viewModel.AllowNew = true;
            }

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _withdraw.GetByAF(AF);
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
                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto(itemId, id, applicantId);
                ancestralTabletTransactionDto.AncestralTabletDtoId = id;
                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
            }
            else
            {
                viewModel.AncestralTabletTransactionDto = Mapper.Map<AncestralTabletTransactionDto>(_withdraw.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestralTabletTransactionsFormViewModel viewModel)
        {
            var ancestralTabletTransaction = Mapper.Map<Core.Domain.AncestralTabletTransaction>(viewModel.AncestralTabletTransactionDto);
            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_withdraw.Add(ancestralTabletTransaction))
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
                _withdraw.Change(ancestralTabletTransaction.AF, ancestralTabletTransaction);
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
            _withdraw.Remove(AF);

            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }
    }
}