using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Cemetery;
using Memorial.Lib.Deceased;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

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

            var plot = _plot.GetById(id);
            var item = _item.GetById(itemId);

            var viewModel = new CemeteryItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                CemeteryItemDto = Mapper.Map<CemeteryItemDto>(item),
                PlotDto = Mapper.Map<PlotDto>(plot),
                PlotId = id,
                CemeteryTransactionDtos = _secondBurial.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId != null && plot.ApplicantId != null && _deceased.GetByPlotId(id).Count() < plot.PlotType.NumberOfPlacement)
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
            var transaction = _secondBurial.GetByAF(AF);
            var item = _item.GetById(transaction.CemeteryItemId);

            var viewModel = new CemeteryTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = item.SubProductService.Name;
            viewModel.PlotDto = Mapper.Map<PlotDto>(transaction.Plot);
            viewModel.CemeteryTransactionDto = Mapper.Map<CemeteryTransactionDto>(transaction);
            viewModel.ApplicantId = transaction.ApplicantId;
            viewModel.DeceasedId = transaction.Deceased1Id;
            viewModel.Header = transaction.Plot.CemeteryArea.Site.Header;

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
            var plot = _plot.GetById(id);
            var viewModel = new CemeteryTransactionsFormViewModel()
            {
                DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(applicantId)),
                PlotDto = Mapper.Map<PlotDto>(plot)
            };

            if (AF == null)
            {
                var cemeteryTransactionDto = new CemeteryTransactionDto(itemId, id, applicantId);
                cemeteryTransactionDto.PlotDtoId = id;
                viewModel.CemeteryTransactionDto = cemeteryTransactionDto;
                viewModel.CemeteryTransactionDto.Price = plot.Price;
            }
            else
            {
                viewModel.CemeteryTransactionDto = Mapper.Map<CemeteryTransactionDto>(_secondBurial.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id == null)
            {
                ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "請選擇 Please Select");
                return FormForResubmit(viewModel);
            }

            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id != null)
            {
                var deceased = _deceased.GetById((int)viewModel.CemeteryTransactionDto.DeceasedDto1Id);
                if (deceased.PlotId != null && (deceased.PlotId != viewModel.CemeteryTransactionDto.PlotDtoId ||
                    _secondBurial.GetTransactionsByPlotIdAndDeceased1Id(viewModel.CemeteryTransactionDto.PlotDtoId, (int)viewModel.CemeteryTransactionDto.DeceasedDto1Id).AF != viewModel.CemeteryTransactionDto.AF))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            var cemeteryTransaction = Mapper.Map<Core.Domain.CemeteryTransaction>(viewModel.CemeteryTransactionDto);
            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                if (_secondBurial.Add(cemeteryTransaction))
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
                if (_invoice.GetByAF(viewModel.CemeteryTransactionDto.AF).Any() &&
                    viewModel.CemeteryTransactionDto.Price <
                _invoice.GetByAF(viewModel.CemeteryTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Price", "* Exceed invoice amount");

                    return FormForResubmit(viewModel);
                }

                _secondBurial.Change(cemeteryTransaction.AF, cemeteryTransaction);
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
            viewModel.DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(viewModel.CemeteryTransactionDto.ApplicantDtoId));

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _secondBurial.Remove(AF);
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