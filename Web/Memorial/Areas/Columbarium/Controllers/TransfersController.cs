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
        private readonly ICentre _centre;
        private readonly IItem _item;
        private readonly ITransfer _transfer;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public TransfersController(
            INiche niche,
            ICentre centre,
            IItem item,
            IApplicant applicant,
            ITransfer transfer,
            ITracking tracking,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _centre = centre;
            _item = item;
            _applicant = applicant;
            _transfer = transfer;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int? applicantId, string filter, int? page)
        {
            _niche.SetNiche(id);
            _item.SetItem(itemId);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                ColumbariumItemId = itemId,
                ColumbariumItemName = _item.GetName(),
                NicheDto = _niche.GetNicheDto(),
                NicheId = id,
                ColumbariumTransactionDtos = _transfer.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            viewModel.AllowNew = applicantId != null
                && _niche.HasApplicant()
                && _niche.GetApplicantId() != applicantId
                && _transfer.AllowNicheDeceasePairing(id, (int)applicantId)
                && !_niche.HasFreeOrder();

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _transfer.SetTransaction(AF);
            _niche.SetNiche(_transfer.GetTransactionNicheId());

            var viewModel = new ColumbariumTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _transfer.GetItemName();
            viewModel.NicheDto = _niche.GetNicheDto();
            viewModel.ColumbariumTransactionDto = _transfer.GetTransactionDto();
            viewModel.ApplicantId = _transfer.GetTransactionApplicantId();
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

                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.ApplicantDto = _applicant.GetApplicantDto(applicantId);
                columbariumTransactionDto.NicheDto = _niche.GetNicheDto();
                columbariumTransactionDto.TransferredApplicantDtoId = columbariumTransactionDto.NicheDto.ApplicantDtoId;

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
                _niche.SetNiche(viewModel.ColumbariumTransactionDto.NicheDtoId);

                if (_niche.GetApplicantId() == viewModel.ColumbariumTransactionDto.ApplicantDtoId)
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.ApplicantDto.Name", "Not allow to be same applicant");
                    return FormForResubmit(viewModel);
                }

                if(!_transfer.AllowNicheDeceasePairing(viewModel.ColumbariumTransactionDto.NicheDtoId, viewModel.ColumbariumTransactionDto.ApplicantDtoId))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.ApplicantDto.Name", "Deceased not linked with new applicant");
                    return FormForResubmit(viewModel);
                }

                if (_transfer.Create(viewModel.ColumbariumTransactionDto))
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


                _transfer.Update(viewModel.ColumbariumTransactionDto);
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
            viewModel.ColumbariumTransactionDto.ApplicantDto = _applicant.GetApplicantDto(viewModel.ColumbariumTransactionDto.ApplicantDtoId);
            viewModel.ColumbariumTransactionDto.NicheDto = _niche.GetNicheDto(viewModel.ColumbariumTransactionDto.NicheDtoId);

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