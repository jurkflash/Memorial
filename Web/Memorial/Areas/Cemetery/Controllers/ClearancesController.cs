using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Cemetery;
using Memorial.Lib.FuneralCompany;
using Memorial.Lib.Deceased;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

namespace Memorial.Areas.Cemetery.Controllers
{
    public class ClearancesController : Controller
    {
        private readonly IPlot _plot;
        private readonly IArea _area;
        private readonly IDeceased _deceased;
        private readonly IItem _item;
        private readonly IClearance _clearance;
        private readonly IFuneralCompany _funeralCompany;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IPlot _invoice;

        public ClearancesController(
            IPlot plot,
            IArea area,
            IDeceased deceased,
            IFuneralCompany funeralCompany,
            IItem item,
            IClearance clearance,
            ITracking tracking,
            Lib.Invoice.IPlot invoice
            )
        {
            _plot = plot;
            _area = area;
            _deceased = deceased;
            _funeralCompany = funeralCompany;
            _item = item;
            _clearance = clearance;
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
                CemeteryTransactionDtos = _clearance.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId != null && plot.ApplicantId != null && _deceased.GetByPlotId(id).Count() > 0 && !plot.hasCleared)
            {
                viewModel.AllowNew = true;
            }
            else
            {
                viewModel.AllowNew = false;
            }

            return View("Index", viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false, string type = "Info")
        {
            var transaction = _clearance.GetByAF(AF);
            var item = _item.GetById(transaction.CemeteryItemId);

            var viewModel = new CemeteryTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = item.SubProductService.Name;
            viewModel.PlotDto = Mapper.Map<PlotDto>(transaction.Plot);
            viewModel.CemeteryTransactionDto = Mapper.Map<CemeteryTransactionDto>(transaction);
            viewModel.ApplicantId = transaction.ApplicantId;
            viewModel.Header = transaction.Plot.CemeteryArea.Site.Header;

            return View(type, viewModel);
        }

        public ActionResult PrintAll(string AF, string type)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true, type = type });
            report.Cookies = cookieCollection;

            return report;
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var plot = _plot.GetById(id);
            var viewModel = new CemeteryTransactionsFormViewModel()
            {
                PlotDto = Mapper.Map<PlotDto>(plot),
                DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(applicantId)),
                FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll())
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
                viewModel.CemeteryTransactionDto = Mapper.Map<CemeteryTransactionDto>(_clearance.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            var cemeteryTransaction = Mapper.Map<Core.Domain.CemeteryTransaction>(viewModel.CemeteryTransactionDto);
            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                if (_clearance.Add(cemeteryTransaction))
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
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.GetByAF(viewModel.CemeteryTransactionDto.AF).Any() &&
                    viewModel.CemeteryTransactionDto.Price <
                _invoice.GetByAF(viewModel.CemeteryTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Price", "* Exceed invoice amount");

                    return View("Form", viewModel);
                }

                _clearance.Change(cemeteryTransaction.AF, cemeteryTransaction);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.CemeteryTransactionDto.CemeteryItemDtoId,
                id = viewModel.CemeteryTransactionDto.PlotDtoId,
                applicantId = viewModel.CemeteryTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _clearance.Remove(AF);
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