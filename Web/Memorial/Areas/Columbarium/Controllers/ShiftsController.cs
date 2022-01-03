using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly INiche _niche;
        private readonly ICentre _centre;
        private readonly IItem _item;
        private readonly IShift _shift;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public ShiftsController(
            INiche niche,
            ICentre centre,
            IItem item,
            IShift shift,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _centre = centre;
            _item = item;
            _shift = shift;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int? applicantId, string filter, int? page)
        {
            var columbariumTransactionDtos = _shift.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage);

            _item.SetItem(itemId);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                ColumbariumItemId = itemId,
                ColumbariumItemName = _item.GetName(),
                ColumbariumTransactionDtos = columbariumTransactionDtos
            };

            _niche.SetNiche(id);

            viewModel.AllowNew = applicantId != null && !_niche.HasFreeOrder();

            viewModel.NicheDto = _niche.GetNicheDto();

            viewModel.NicheId = id;

            return View("ShiftedToIndex", viewModel);

        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _shift.SetTransaction(AF);
            _niche.SetNiche(_shift.GetTransactionNicheId());

            var viewModel = new ColumbariumTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _shift.GetItemName();
            viewModel.NicheDto = _niche.GetNicheDto();
            viewModel.ColumbariumTransactionDto = _shift.GetTransactionDto();
            viewModel.ApplicantId = _shift.GetTransactionApplicantId();
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
            var viewModel = new ColumbariumTransactionsFormViewModel();

            if (AF == null)
            {
                _niche.SetNiche(id);

                var columbariumTransactionDto = new ColumbariumTransactionDto();

                columbariumTransactionDto.ApplicantDtoId = applicantId;

                columbariumTransactionDto.ColumbariumItemDtoId = itemId;

                columbariumTransactionDto.ShiftedNicheDtoId = id;
                columbariumTransactionDto.ShiftedNicheDto = _niche.GetNicheDto();

                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
            }
            else
            {
                viewModel.ColumbariumTransactionDto = _shift.GetTransactionDto(AF);

                _niche.SetNiche((int)viewModel.ColumbariumTransactionDto.ShiftedNicheDtoId);

                viewModel.ColumbariumTransactionDto.ShiftedNicheDto = _niche.GetNicheDto();
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                if (_shift.Create(viewModel.ColumbariumTransactionDto))
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
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.ColumbariumTransactionDto.AF).Any() &&
                    viewModel.ColumbariumTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.ColumbariumTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _shift.Update(viewModel.ColumbariumTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemDtoId,
                id = viewModel.ColumbariumTransactionDto.NicheDtoId,
                applicantId = viewModel.ColumbariumTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult FormForResubmit(ColumbariumTransactionsFormViewModel viewModel)
        {
            if(viewModel.ColumbariumTransactionDto.ShiftedNicheDtoId != null)
            {
                _niche.SetNiche((int)viewModel.ColumbariumTransactionDto.ShiftedNicheDtoId);

                viewModel.ColumbariumTransactionDto.ShiftedNicheDto = _niche.GetNicheDto();
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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Columbarium" });
        }
    }
}