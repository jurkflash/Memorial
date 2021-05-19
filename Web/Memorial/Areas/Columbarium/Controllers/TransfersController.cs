using System.Linq;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class TransfersController : Controller
    {
        private readonly INiche _niche;
        private readonly ITransfer _transfer;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public TransfersController(
            INiche niche,
            IApplicant applicant,
            ITransfer transfer,
            ITracking tracking,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _applicant = applicant;
            _transfer = transfer;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            _niche.SetNiche(id);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                ColumbariumItemId = itemId,
                NicheDto = _niche.GetNicheDto(),
                NicheId = id,
                ColumbariumTransactionDtos = _transfer.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            viewModel.AllowNew = applicantId != 0
                && _niche.HasApplicant()
                && _niche.GetApplicantId() != applicantId
                && _transfer.AllowNicheDeceasePairing(id, applicantId)
                && !_niche.HasFreeOrder();

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _transfer.SetTransaction(AF);
            _niche.SetNiche(_transfer.GetTransactionNicheId());

            var viewModel = new ColumbariumTransactionsInfoViewModel()
            {
                ApplicantId = _transfer.GetTransactionApplicantId(),
                DeceasedId = _transfer.GetTransactionDeceased1Id(),
                NicheDto = _niche.GetNicheDto(),
                ItemName = _transfer.GetItemName(),
                ColumbariumTransactionDto = _transfer.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new ColumbariumTransactionsFormViewModel();

            if (AF == null)
            {
                _niche.SetNiche(id);

                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.Applicant = _applicant.GetApplicant(applicantId);
                columbariumTransactionDto.Niche = _niche.GetNiche();
                columbariumTransactionDto.TransferredApplicantId = columbariumTransactionDto.Niche.ApplicantId;

                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
                viewModel.ColumbariumTransactionDto.Price = _transfer.GetItemPrice(itemId);
            }
            else
            {
                _transfer.SetTransaction(AF);
                viewModel.ColumbariumTransactionDto = _transfer.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                _niche.SetNiche(viewModel.ColumbariumTransactionDto.NicheId);

                if (_niche.GetApplicantId() == viewModel.ColumbariumTransactionDto.ApplicantId)
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Applicant.Name", "Not allow to be same applicant");
                    return FormForResubmit(viewModel);
                }

                if(!_transfer.AllowNicheDeceasePairing(viewModel.ColumbariumTransactionDto.NicheId, viewModel.ColumbariumTransactionDto.ApplicantId))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Applicant.Name", "Deceased not linked with new applicant");
                    return FormForResubmit(viewModel);
                }

                if (_transfer.Create(viewModel.ColumbariumTransactionDto))
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


                _transfer.Update(viewModel.ColumbariumTransactionDto);
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
            viewModel.ColumbariumTransactionDto.Applicant = _applicant.GetApplicant(viewModel.ColumbariumTransactionDto.ApplicantId);
            viewModel.ColumbariumTransactionDto.Niche = _niche.GetNiche(viewModel.ColumbariumTransactionDto.NicheId);

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _transfer.SetTransaction(AF);
                _transfer.Delete();
            }

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