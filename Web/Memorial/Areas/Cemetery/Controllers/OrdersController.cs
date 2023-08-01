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

            _plot.SetPlot(id);
            _item.SetItem(itemId);

            var viewModel = new CemeteryItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                CemeteryItemDto = _item.GetItemDto(),
                PlotDto = _plot.GetPlotDto(),
                PlotId = id,
                CemeteryTransactionDtos = _order.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId == null || _plot.HasApplicant() || _plot.HasCleared())
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
            _order.SetTransaction(AF);
            _plot.SetPlot(_order.GetTransactionPlotId());
            _area.SetArea(_plot.GetAreaId());

            var viewModel = new CemeteryTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _order.GetItemName();
            viewModel.PlotDto = _plot.GetPlotDto();
            viewModel.CemeteryTransactionDto = _order.GetTransactionDto();
            viewModel.ApplicantId = _order.GetTransactionApplicantId();
            viewModel.DeceasedId = _order.GetTransactionDeceased1Id();
            viewModel.Header = _area.GetArea().Site.Header;

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
            _plot.SetPlot(id);

            var viewModel = new CemeteryTransactionsFormViewModel()
            {
                PlotDto = _plot.GetPlotDto(),
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId),
                FuneralCompanyDtos = _funeralCompany.GetFuneralCompanyDtos()
            };

            if (AF == null)
            {
                var cemeteryTransactionDto = new CemeteryTransactionDto(itemId, id, applicantId);
                cemeteryTransactionDto.PlotDtoId = id;
                viewModel.CemeteryTransactionDto = cemeteryTransactionDto;
                viewModel.CemeteryTransactionDto.Price = _plot.GetPrice();
                viewModel.CemeteryTransactionDto.Maintenance = _plot.GetMaintenance();
                viewModel.CemeteryTransactionDto.Brick = _plot.GetBrick();
                viewModel.CemeteryTransactionDto.Dig = _plot.GetDig();
                viewModel.CemeteryTransactionDto.Wall = _plot.GetWall();
            }
            else
            {
                _order.SetTransaction(AF);
                viewModel.CemeteryTransactionDto = _order.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            _plot.SetPlot(viewModel.CemeteryTransactionDto.PlotDtoId);
            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id == null && !_plot.IsFengShuiPlot())
            {
                ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "請選擇 Please Select");
                return FormForResubmit(viewModel);
            }

            if (viewModel.CemeteryTransactionDto.DeceasedDto1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.CemeteryTransactionDto.DeceasedDto1Id);
                if (_deceased.GetPlot() != null && _deceased.GetPlot().Id != viewModel.CemeteryTransactionDto.PlotDtoId)
                {
                    ModelState.AddModelError("CemeteryTransactionDto.DeceasedDto1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                if (_order.Create(viewModel.CemeteryTransactionDto))
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
                    viewModel.CemeteryTransactionDto.Price + 
                    (float)viewModel.CemeteryTransactionDto.Maintenance + 
                    (float)viewModel.CemeteryTransactionDto.Brick + 
                    (float)viewModel.CemeteryTransactionDto.Dig + 
                    (float)viewModel.CemeteryTransactionDto.Wall
                    <
                _invoice.GetInvoicesByAF(viewModel.CemeteryTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("CemeteryTransactionDto.Maintenance", "* Exceed invoice amount");
                    ModelState.AddModelError("CemeteryTransactionDto.Brick", "* Exceed invoice amount");
                    ModelState.AddModelError("CemeteryTransactionDto.Dig", "* Exceed invoice amount");
                    ModelState.AddModelError("CemeteryTransactionDto.Wall", "* Exceed invoice amount");

                    return FormForResubmit(viewModel);
                }

                _order.Update(viewModel.CemeteryTransactionDto);
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
            viewModel.FuneralCompanyDtos = _funeralCompany.GetFuneralCompanyDtos();

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _order.SetTransaction(AF);
                _order.Delete();
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