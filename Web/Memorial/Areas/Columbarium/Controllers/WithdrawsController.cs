using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

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

            _niche.SetNiche(id);
            _item.SetItem(itemId);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                ColumbariumItemDto = _item.GetItemDto(),
                NicheDto = _niche.GetNicheDto(),
                NicheId = id,
                ColumbariumTransactionDtos = _withdraw.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            if(applicantId != null && !_withdraw.GetTransactionDtosByNicheIdAndItemId(id, itemId, null).Any())
            {
                viewModel.AllowNew = true;
            }

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _withdraw.SetTransaction(AF);
            _niche.SetNiche(_withdraw.GetTransactionNicheId());
            _centre.SetCentre(_niche.GetNiche().ColumbariumArea.ColumbariumCentreId);

            var viewModel = new ColumbariumTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _withdraw.GetItemName();
            viewModel.NicheDto = _niche.GetNicheDto();
            viewModel.ColumbariumTransactionDto = _withdraw.GetTransactionDto();
            viewModel.ColumbariumCentreDto = _centre.GetCentreDto();
            viewModel.ApplicantId = _withdraw.GetTransactionApplicantId();
            viewModel.Header = _centre.GetCentre().Site.Header;

            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            return report;
        }


        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var item = _item.GetItemDto(itemId);
            var viewModel = new ColumbariumTransactionsFormViewModel();
            viewModel.ColumbariumCentreDto = item.ColumbariumCentreDto;

            if (AF == null)
            {
                _niche.SetNiche(id);

                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.NicheDtoId = id;
                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
            }
            else
            {
                _withdraw.SetTransaction(AF);
                viewModel.ColumbariumTransactionDto = _withdraw.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                if (_withdraw.Create(viewModel.ColumbariumTransactionDto))
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
                _withdraw.Update(viewModel.ColumbariumTransactionDto);
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
            _withdraw.SetTransaction(AF);
            _withdraw.Delete();

            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }

    }
}