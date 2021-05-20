using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Cemetery;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Cemetery.Controllers
{
    public class FengShuiTransfersController : Controller
    {
        private readonly IPlot _plot;
        private readonly ITransfer _transfer;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IPlot _invoice;

        public FengShuiTransfersController(
            IPlot plot,
            IApplicant applicant,
            ITransfer transfer,
            ITracking tracking,
            Lib.Invoice.IPlot invoice
            )
        {
            _plot = plot;
            _applicant = applicant;
            _transfer = transfer;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _plot.SetPlot(id);

            var viewModel = new CemeteryItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                CemeteryItemId = itemId,
                PlotDto = _plot.GetPlotDto(),
                PlotId = id,
                CemeteryTransactionDtos = _transfer.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            viewModel.AllowNew = applicantId != 0 && _plot.HasApplicant() && _plot.GetApplicantId() != applicantId && _transfer.AllowPlotDeceasePairing(_plot, applicantId);

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _transfer.SetTransaction(AF);
            _plot.SetPlot(_transfer.GetTransactionPlotId());

            var viewModel = new CemeteryTransactionsInfoViewModel()
            {
                ApplicantId = _transfer.GetTransactionApplicantId(),
                DeceasedId = _transfer.GetTransactionDeceased1Id(),
                PlotDto = _plot.GetPlotDto(),
                ItemName = _transfer.GetItemName(),
                CemeteryTransactionDto = _transfer.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new CemeteryTransactionsFormViewModel();

            if (AF == null)
            {
                _plot.SetPlot(id);

                var cemeteryTransactionDto = new CemeteryTransactionDto(itemId, id, applicantId);
                cemeteryTransactionDto.ApplicantDto = _applicant.GetApplicantDto(applicantId);
                cemeteryTransactionDto.PlotDto = _plot.GetPlotDto();

                viewModel.CemeteryTransactionDto = cemeteryTransactionDto;
                viewModel.CemeteryTransactionDto.Price = _transfer.GetItemPrice(itemId);
            }
            else
            {
                _transfer.SetTransaction(AF);
                viewModel.CemeteryTransactionDto = _transfer.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                _plot.SetPlot(viewModel.CemeteryTransactionDto.PlotDtoId);

                if (_plot.GetApplicantId() == viewModel.CemeteryTransactionDto.ApplicantDtoId)
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Applicant.Name", "Not allow to be same applicant");
                    return FormForResubmit(viewModel);
                }

                if(!_transfer.AllowPlotDeceasePairing(_plot, viewModel.CemeteryTransactionDto.ApplicantDtoId))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Applicant.Name", "Deceased not linked with new applicant");
                    return FormForResubmit(viewModel);
                }

                if (_transfer.Create(viewModel.CemeteryTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.CemeteryTransactionDto.CemeteryItemId,
                        id = viewModel.CemeteryTransactionDto.PlotDtoId,
                        applicantId = viewModel.CemeteryTransactionDto.ApplicantDtoId
                    });
                }
                else
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.CemeteryTransactionDto.AF).Any() &&
                    viewModel.CemeteryTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.CemeteryTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }


                _transfer.Update(viewModel.CemeteryTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.CemeteryTransactionDto.CemeteryItemId,
                id = viewModel.CemeteryTransactionDto.PlotDtoId,
                applicantId = viewModel.CemeteryTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult FormForResubmit(CemeteryTransactionsFormViewModel viewModel)
        {
            viewModel.CemeteryTransactionDto.ApplicantDto = _applicant.GetApplicantDto(viewModel.CemeteryTransactionDto.ApplicantDtoId);
            viewModel.CemeteryTransactionDto.PlotDto = _plot.GetPlotDto(viewModel.CemeteryTransactionDto.PlotDtoId);

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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Plot" });
            }
    }
}