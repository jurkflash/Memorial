using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class ManageController : Controller
    {
        private readonly INiche _niche;
        private readonly ICentre _centre;
        private readonly IItem _item;
        private readonly IManage _manage;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public ManageController(
            INiche niche,
            ICentre centre,
            IItem item,
            IManage manage,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _centre = centre;
            _item = item;
            _manage = manage;
            _invoice = invoice;
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
                ColumbariumTransactionDtos = _manage.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null && _niche.HasApplicant()
            };
            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _manage.SetTransaction(AF);
            _niche.SetNiche(_manage.GetTransactionNicheId());
            _centre.SetCentre(_niche.GetNiche().ColumbariumArea.ColumbariumCentreId);

            var viewModel = new ColumbariumTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _manage.GetItemName();
            viewModel.NicheDto = _niche.GetNicheDto();
            viewModel.ColumbariumTransactionDto = _manage.GetTransactionDto();
            viewModel.ApplicantId = _manage.GetTransactionApplicantId();
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
                viewModel.ColumbariumTransactionDto.Price = _manage.GetPrice(itemId);
            }
            else
            {
                _manage.SetTransaction(AF);
                viewModel.ColumbariumTransactionDto = _manage.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                if (_manage.Create(viewModel.ColumbariumTransactionDto))
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
                if (_invoice.GetInvoicesByAF(viewModel.ColumbariumTransactionDto.AF).Any() && 
                    viewModel.ColumbariumTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.ColumbariumTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Price", "* Exceed invoice amount");
                    return View("Form", viewModel);
                }


                _manage.Update(viewModel.ColumbariumTransactionDto);
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
            _manage.SetTransaction(AF);
            _manage.Delete();

            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Columbarium" });
        }
    }
}