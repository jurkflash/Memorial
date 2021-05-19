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
        private readonly IQuadrangle _quadrangle;
        private readonly ITransfer _transfer;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IQuadrangle _invoice;

        public TransfersController(
            IQuadrangle quadrangle,
            IApplicant applicant,
            ITransfer transfer,
            ITracking tracking,
            Lib.Invoice.IQuadrangle invoice
            )
        {
            _quadrangle = quadrangle;
            _applicant = applicant;
            _transfer = transfer;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            _quadrangle.SetQuadrangle(id);

            var viewModel = new QuadrangleItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                QuadrangleItemId = itemId,
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                QuadrangleId = id,
                QuadrangleTransactionDtos = _transfer.GetTransactionDtosByQuadrangleIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            viewModel.AllowNew = applicantId != 0
                && _quadrangle.HasApplicant()
                && _quadrangle.GetApplicantId() != applicantId
                && _transfer.AllowQuadrangleDeceasePairing(id, applicantId)
                && !_quadrangle.HasFreeOrder();

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _transfer.SetTransaction(AF);
            _quadrangle.SetQuadrangle(_transfer.GetTransactionQuadrangleId());

            var viewModel = new QuadrangleTransactionsInfoViewModel()
            {
                ApplicantId = _transfer.GetTransactionApplicantId(),
                DeceasedId = _transfer.GetTransactionDeceased1Id(),
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                ItemName = _transfer.GetItemName(),
                QuadrangleTransactionDto = _transfer.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new QuadrangleTransactionsFormViewModel();

            if (AF == null)
            {
                _quadrangle.SetQuadrangle(id);

                var quadrangleTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                quadrangleTransactionDto.Applicant = _applicant.GetApplicant(applicantId);
                quadrangleTransactionDto.Quadrangle = _quadrangle.GetQuadrangle();
                quadrangleTransactionDto.TransferredApplicantId = quadrangleTransactionDto.Quadrangle.ApplicantId;

                viewModel.QuadrangleTransactionDto = quadrangleTransactionDto;
                viewModel.QuadrangleTransactionDto.Price = _transfer.GetItemPrice(itemId);
            }
            else
            {
                _transfer.SetTransaction(AF);
                viewModel.QuadrangleTransactionDto = _transfer.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            if (viewModel.QuadrangleTransactionDto.AF == null)
            {
                _quadrangle.SetQuadrangle(viewModel.QuadrangleTransactionDto.QuadrangleId);

                if (_quadrangle.GetApplicantId() == viewModel.QuadrangleTransactionDto.ApplicantId)
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Applicant.Name", "Not allow to be same applicant");
                    return FormForResubmit(viewModel);
                }

                if(!_transfer.AllowQuadrangleDeceasePairing(viewModel.QuadrangleTransactionDto.QuadrangleId, viewModel.QuadrangleTransactionDto.ApplicantId))
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Applicant.Name", "Deceased not linked with new applicant");
                    return FormForResubmit(viewModel);
                }

                if (_transfer.Create(viewModel.QuadrangleTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.QuadrangleTransactionDto.QuadrangleItemId,
                        id = viewModel.QuadrangleTransactionDto.QuadrangleId,
                        applicantId = viewModel.QuadrangleTransactionDto.ApplicantId
                    });
                }
                else
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.QuadrangleTransactionDto.AF).Any() &&
                    viewModel.QuadrangleTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.QuadrangleTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }


                _transfer.Update(viewModel.QuadrangleTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.QuadrangleTransactionDto.QuadrangleItemId,
                id = viewModel.QuadrangleTransactionDto.QuadrangleId,
                applicantId = viewModel.QuadrangleTransactionDto.ApplicantId
            });
        }

        public ActionResult FormForResubmit(QuadrangleTransactionsFormViewModel viewModel)
        {
            viewModel.QuadrangleTransactionDto.Applicant = _applicant.GetApplicant(viewModel.QuadrangleTransactionDto.ApplicantId);
            viewModel.QuadrangleTransactionDto.Quadrangle = _quadrangle.GetQuadrangle(viewModel.QuadrangleTransactionDto.QuadrangleId);

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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Quadrangle" });
        }
    }
}