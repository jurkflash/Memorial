using System.Linq;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Cemetery;
using Memorial.Lib.FuneralCompany;
using Memorial.Lib.Deceased;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;
using System.Collections.Generic;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Areas.Cemetery.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IPlot _plot;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IDeceased _deceased;
        private readonly IOrder _order;
        private readonly IApplicant _applicant;
        private readonly IFuneralCompany _funeralCompany;
        private readonly ITracking _tracking;
        private readonly IPlotApplicantDeceaseds _plotApplicantDeceaseds;
        private readonly Lib.Invoice.IPlot _invoice;

        public OrdersController(
            IPlot plot,
            IArea area,
            IItem item,
            IApplicant applicant,
            IFuneralCompany funeralCompany,
            IDeceased deceased,
            IOrder order,
            ITracking tracking,
            IPlotApplicantDeceaseds plotApplicantDeceaseds,
            Lib.Invoice.IPlot invoice
            )
        {
            _plot = plot;
            _area = area;
            _item = item;
            _applicant = applicant;
            _funeralCompany = funeralCompany;
            _deceased = deceased;
            _order = order;
            _tracking = tracking;
            _plotApplicantDeceaseds = plotApplicantDeceaseds;
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
                CemeteryTransactionDtos = _order.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId == null || plot.ApplicantId != null || plot.hasCleared)
            {
                viewModel.AllowNew = false;
            }
            else
            {
                viewModel.AllowNew = true;
            }

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _order.GetByAF(AF);
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

        public ActionResult PrintRuleCN()
        {
            var report = new Rotativa.ViewAsPdf("InfoRuleCN");
            return report;
        }

        public ActionResult PrintRuleBM()
        {
            var report = new Rotativa.ViewAsPdf("InfoRuleBM");
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
                viewModel.CemeteryTransactionDto.Maintenance = plot.Maintenance;
                viewModel.CemeteryTransactionDto.Brick = plot.Brick;
                viewModel.CemeteryTransactionDto.Dig = plot.Dig;
                viewModel.CemeteryTransactionDto.Wall = plot.Wall;
            }
            else
            {
                viewModel.CemeteryTransactionDto = Mapper.Map<CemeteryTransactionDto>(_order.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            var plot = _plot.GetById(viewModel.CemeteryTransactionDto.PlotDtoId);
            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id == null && !plot.PlotType.isFengShuiPlot)
            {
                ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "請選擇 Please Select");
                return FormForResubmit(viewModel);
            }

            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id != null)
            {
                var deceased = _deceased.GetById((int)viewModel.CemeteryTransactionDto.DeceasedDto1Id);
                if (deceased.PlotId != null && deceased.PlotId != viewModel.CemeteryTransactionDto.PlotDtoId)
                {
                    ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            var cemeteryTransaction = Mapper.Map<Core.Domain.CemeteryTransaction>(viewModel.CemeteryTransactionDto);
            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                if (_order.Add(cemeteryTransaction))
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
                    viewModel.CemeteryTransactionDto.Price + 
                    (float)viewModel.CemeteryTransactionDto.Maintenance + 
                    (float)viewModel.CemeteryTransactionDto.Brick + 
                    (float)viewModel.CemeteryTransactionDto.Dig + 
                    (float)viewModel.CemeteryTransactionDto.Wall
                    <
                _invoice.GetByAF(viewModel.CemeteryTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("CemeteryTransactionDto.Maintenance", "* Exceed invoice amount");
                    ModelState.AddModelError("CemeteryTransactionDto.Brick", "* Exceed invoice amount");
                    ModelState.AddModelError("CemeteryTransactionDto.Dig", "* Exceed invoice amount");
                    ModelState.AddModelError("CemeteryTransactionDto.Wall", "* Exceed invoice amount");

                    return FormForResubmit(viewModel);
                }

                _order.Change(cemeteryTransaction.AF, cemeteryTransaction);
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
            viewModel.FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll());
            viewModel.DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(viewModel.CemeteryTransactionDto.ApplicantDtoId));

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _order.Remove(AF);
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