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
        private readonly IShift _shift;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public ShiftsController(
            INiche niche,
            IShift shift,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _shift = shift;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            var columbariumTransactionDtos = _shift.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                ColumbariumItemId = itemId,
                ColumbariumTransactionDtos = columbariumTransactionDtos
            };

            _niche.SetNiche(id);

            viewModel.AllowNew = applicantId != 0 && !_niche.HasFreeOrder();

            viewModel.NicheDto = _niche.GetNicheDto();

            viewModel.NicheId = id;

            return View("ShiftedToIndex", viewModel);

        }

        public ActionResult Info(string AF)
        {
            _shift.SetTransaction(AF);
            _niche.SetNiche(_shift.GetTransactionNicheId());

            var viewModel = new ColumbariumTransactionsInfoViewModel()
            {
                ApplicantId = _shift.GetTransactionApplicantId(),
                DeceasedId = _shift.GetTransactionDeceased1Id(),
                NicheDto = _niche.GetNicheDto(),
                ItemName = _shift.GetItemName(),
                ColumbariumTransactionDto = _shift.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new ColumbariumTransactionsFormViewModel();

            if (AF == null)
            {
                _niche.SetNiche(id);

                var columbariumTransactionDto = new ColumbariumTransactionDto();

                columbariumTransactionDto.ApplicantId = applicantId;

                columbariumTransactionDto.ColumbariumItemId = itemId;

                columbariumTransactionDto.ShiftedNicheId = id;
                columbariumTransactionDto.ShiftedNiche = _niche.GetNiche();

                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
            }
            else
            {
                viewModel.ColumbariumTransactionDto = _shift.GetTransactionDto(AF);

                _niche.SetNiche((int)viewModel.ColumbariumTransactionDto.ShiftedNicheId);

                viewModel.ColumbariumTransactionDto.ShiftedNiche = _niche.GetNiche();
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
                        itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemId,
                        id = viewModel.ColumbariumTransactionDto.NicheId,
                        applicantId = viewModel.ColumbariumTransactionDto.ApplicantId
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
                itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemId,
                id = viewModel.ColumbariumTransactionDto.NicheId,
                applicantId = viewModel.ColumbariumTransactionDto.ApplicantId
            });
        }

        public ActionResult FormForResubmit(ColumbariumTransactionsFormViewModel viewModel)
        {
            if(viewModel.ColumbariumTransactionDto.ShiftedNicheId != null)
            {
                _niche.SetNiche((int)viewModel.ColumbariumTransactionDto.ShiftedNicheId);

                viewModel.ColumbariumTransactionDto.ShiftedNiche = _niche.GetNiche();
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