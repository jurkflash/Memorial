using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class WithdrawsController : Controller
    {
        private readonly INiche _niche;
        private readonly ICentre _centre;
        private readonly IWithdraw _withdraw;
        private readonly IArea _area;
        private readonly IItem _item;

        public WithdrawsController(
            INiche niche,
            ICentre centre,
            IWithdraw withdraw,
            IArea area,
            IItem item
            )
        {
            _niche = niche;
            _centre = centre;
            _withdraw = withdraw;
            _area = area;
            _item = item;
        }

        public ActionResult Index(int itemId, int id, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var niche = _niche.GetById(id);
            var item = _item.GetById(itemId);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                ColumbariumItemDto = Mapper.Map<ColumbariumItemDto>(item),
                NicheDto = Mapper.Map<NicheDto>(niche),
                NicheId = id,
                ColumbariumTransactionDtos = Mapper.Map<IEnumerable<ColumbariumTransactionDto>>(_withdraw.GetByNicheIdAndItemId(id, itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            if(applicantId != null && !_withdraw.GetByNicheIdAndItemId(id, itemId, null).Any())
            {
                viewModel.AllowNew = true;
            }

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _withdraw.GetByAF(AF);
            var niche = _niche.GetById(transaction.NicheId);
            var centre = _centre.GetById(niche.ColumbariumArea.ColumbariumCentreId);

            var viewModel = new ColumbariumTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = transaction.ColumbariumItem.SubProductService.Name;
            viewModel.NicheDto = Mapper.Map<NicheDto>(niche);
            viewModel.ColumbariumTransactionDto = Mapper.Map<ColumbariumTransactionDto>(transaction);
            viewModel.ColumbariumCentreDto = Mapper.Map<ColumbariumCentreDto>(centre);
            viewModel.ApplicantId = transaction.ApplicantId;
            viewModel.Header = centre.Site.Header;

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
            var item = _item.GetById(itemId);
            var viewModel = new ColumbariumTransactionsFormViewModel();
            viewModel.ColumbariumCentreDto = Mapper.Map<ColumbariumCentreDto>(item.ColumbariumCentre);

            if (AF == null)
            {
                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.NicheDtoId = id;
                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
            }
            else
            {
                viewModel.ColumbariumTransactionDto = Mapper.Map<ColumbariumTransactionDto>(_withdraw.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            var columbariumTransaction = Mapper.Map<Core.Domain.ColumbariumTransaction>(viewModel.ColumbariumTransactionDto);
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                if (_withdraw.Add(columbariumTransaction))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemDtoId,
                        id = viewModel.ColumbariumTransactionDto.NicheDtoId,
                        applicantId = viewModel.ColumbariumTransactionDto.ApplicantDtoId
                    });
                }
                else
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                _withdraw.Change(columbariumTransaction.AF, columbariumTransaction);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemDtoId,
                id = viewModel.ColumbariumTransactionDto.NicheDtoId,
                applicantId = viewModel.ColumbariumTransactionDto.ApplicantDtoId
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