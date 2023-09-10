using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Cemetery;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Areas.Cemetery.Controllers
{
    public class FengShuiTransfersController : Controller
    {
        private readonly IPlot _plot;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly ITransfer _transfer;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IPlot _invoice;

        public FengShuiTransfersController(
            IPlot plot,
            IArea area,
            IItem item,
            IApplicant applicant,
            ITransfer transfer,
            ITracking tracking,
            Lib.Invoice.IPlot invoice
            )
        {
            _plot = plot;
            _area = area;
            _item = item;
            _applicant = applicant;
            _transfer = transfer;
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
                CemeteryTransactionDtos = _transfer.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            viewModel.AllowNew = applicantId != null && plot.ApplicantId != null && plot.ApplicantId != applicantId && _transfer.AllowPlotDeceasePairing(plot, (int)applicantId);

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _transfer.GetByAF(AF);
            var item = _item.GetById(transaction.CemeteryItemId);

            var viewModel = new CemeteryTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = item.SubProductService.Name;
            viewModel.PlotDto = Mapper.Map<PlotDto>(transaction.Plot);
            viewModel.CemeteryTransactionDto = Mapper.Map<CemeteryTransactionDto>(transaction);
            viewModel.ApplicantDto = Mapper.Map<ApplicantDto>(_applicant.Get((int)transaction.TransferredApplicantId));
            viewModel.ApplicantId = (int)transaction.TransferredApplicantId;
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
            var viewModel = new CemeteryTransactionsFormViewModel();
            viewModel.PlotDto = Mapper.Map<PlotDto>(plot);

            if (AF == null)
            {
                var cemeteryTransactionDto = new CemeteryTransactionDto(itemId, id, applicantId);
                cemeteryTransactionDto.ApplicantDto = Mapper.Map<ApplicantDto>(_applicant.Get(applicantId));
                cemeteryTransactionDto.PlotDto = Mapper.Map<PlotDto>(plot);
                cemeteryTransactionDto.TransferredApplicantId = plot.ApplicantId;

                var item = _item.GetById(itemId);
                viewModel.CemeteryTransactionDto = cemeteryTransactionDto;
                viewModel.CemeteryTransactionDto.Price = _item.GetPrice(item);
                
            }
            else
            {
                viewModel.CemeteryTransactionDto = Mapper.Map<CemeteryTransactionDto>(_transfer.GetByAF(AF));
                viewModel.ApplicantDto = Mapper.Map<ApplicantDto>(_applicant.Get((int)viewModel.CemeteryTransactionDto.TransferredApplicantId));
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            var cemeteryTransaction = Mapper.Map<Core.Domain.CemeteryTransaction>(viewModel.CemeteryTransactionDto);
            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                var plot = _plot.GetById(viewModel.CemeteryTransactionDto.PlotDtoId);

                if (plot.ApplicantId == viewModel.CemeteryTransactionDto.ApplicantDtoId)
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Applicant.Name", "Not allow to be same applicant");
                    return FormForResubmit(viewModel);
                }

                if(!_transfer.AllowPlotDeceasePairing(plot, viewModel.CemeteryTransactionDto.ApplicantDtoId))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Applicant.Name", "Deceased not linked with new applicant");
                    return FormForResubmit(viewModel);
                }

                if (_transfer.Add(cemeteryTransaction))
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


                _transfer.Change(cemeteryTransaction.AF, cemeteryTransaction);
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
            viewModel.CemeteryTransactionDto.ApplicantDto = Mapper.Map<ApplicantDto>(_applicant.Get(viewModel.CemeteryTransactionDto.ApplicantDtoId));
            viewModel.CemeteryTransactionDto.PlotDto = Mapper.Map<PlotDto>(_plot.GetById(viewModel.CemeteryTransactionDto.PlotDtoId));

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _transfer.Remove(AF);
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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Cemetery" });
            }
    }
}