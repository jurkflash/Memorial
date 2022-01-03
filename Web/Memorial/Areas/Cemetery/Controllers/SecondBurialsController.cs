using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Cemetery;
using Memorial.Lib.Deceased;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Cemetery.Controllers
{
    public class SecondBurialsController : Controller
    {
        private readonly IPlot _plot;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IDeceased _deceased;
        private readonly ISecondBurial _secondBurial;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IPlot _invoice;

        public SecondBurialsController(
            IPlot plot,
            IArea area,
            IItem item,
            IDeceased deceased,
            ISecondBurial secondBurial,
            ITracking tracking,
            Lib.Invoice.IPlot invoice
            )
        {
            _plot = plot;
            _area = area;
            _item = item;
            _deceased = deceased;
            _secondBurial = secondBurial;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _plot.SetPlot(id);
            _item.SetItem(itemId);

            var viewModel = new CemeteryItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                CemeteryItemDto = _item.GetItemDto(),
                PlotDto = _plot.GetPlotDto(),
                PlotId = id,
                CemeteryTransactionDtos = _secondBurial.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId != null && _plot.HasApplicant() && _deceased.GetDeceasedsByPlotId(id).Count() < _plot.GetNumberOfPlacement())
            {
                viewModel.AllowNew = true;
            }
            else
            {
                viewModel.AllowNew = false;
            }

            return View("Index", viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _secondBurial.SetTransaction(AF);
            _plot.SetPlot(_secondBurial.GetTransactionPlotId());
            _area.SetArea(_plot.GetAreaId());

            var viewModel = new CemeteryTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _secondBurial.GetItemName();
            viewModel.PlotDto = _plot.GetPlotDto();
            viewModel.CemeteryTransactionDto = _secondBurial.GetTransactionDto();
            viewModel.ApplicantId = _secondBurial.GetTransactionApplicantId();
            viewModel.DeceasedId = _secondBurial.GetTransactionDeceased1Id();
            viewModel.Header = _area.GetArea().Site.Header;

            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            return report;
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new CemeteryTransactionsFormViewModel()
            {
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            if (AF == null)
            {
                _plot.SetPlot(id);

                var cemeteryTransactionDto = new CemeteryTransactionDto(itemId, id, applicantId);
                cemeteryTransactionDto.PlotDtoId = id;
                viewModel.CemeteryTransactionDto = cemeteryTransactionDto;
                viewModel.CemeteryTransactionDto.Price = _plot.GetPrice();
            }
            else
            {
                _secondBurial.SetTransaction(AF);
                viewModel.CemeteryTransactionDto = _secondBurial.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id == null)
            {
                ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "Please Select");
                return FormForResubmit(viewModel);
            }

            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.CemeteryTransactionDto.DeceasedDto1Id);
                if (_deceased.GetPlot() != null && (_deceased.GetPlot().Id != viewModel.CemeteryTransactionDto.PlotDtoId ||
                    _secondBurial.GetTransactionsByPlotIdAndDeceased1Id(viewModel.CemeteryTransactionDto.PlotDtoId, (int)viewModel.CemeteryTransactionDto.DeceasedDto1Id).AF != viewModel.CemeteryTransactionDto.AF))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                if (_secondBurial.Create(viewModel.CemeteryTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.CemeteryTransactionDto.CemeteryItemDtoId,
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

                _secondBurial.Update(viewModel.CemeteryTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.CemeteryTransactionDto.CemeteryItemDtoId,
                id = viewModel.CemeteryTransactionDto.PlotDtoId,
                applicantId = viewModel.CemeteryTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult FormForResubmit(CemeteryTransactionsFormViewModel viewModel)
        {
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.CemeteryTransactionDto.ApplicantDtoId);

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _secondBurial.SetTransaction(AF);
                _secondBurial.Delete();
            }

            return RedirectToAction("Index", new
            {
                itemId,
                id,
                applicantId
            });
        }

        public ActionResult Invoices(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Cemetery" });
        }

    }
}