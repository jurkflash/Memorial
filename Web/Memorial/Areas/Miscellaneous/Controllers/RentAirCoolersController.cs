using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Miscellaneous;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

namespace Memorial.Areas.Miscellaneous.Controllers
{
    public class RentAirCoolersController : Controller
    {
        private readonly IMiscellaneous _miscellaneous;
        private readonly IItem _item;
        private readonly IRentAirCooler _rentAirCooler;

        public RentAirCoolersController(
            IMiscellaneous miscellaneous,
            IItem item,
            IRentAirCooler rentAirCooler
            )
        {
            _miscellaneous = miscellaneous;
            _item = item;
            _rentAirCooler = rentAirCooler;
        }

        public ActionResult Index(int itemId, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var item = _item.GetById(itemId);
            var viewModel = new MiscellaneousItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                MiscellaneousItemDto = Mapper.Map<MiscellaneousItemDto>(item),
                MiscellaneousTransactionDtos = Mapper.Map<IEnumerable<MiscellaneousTransactionDto>>(_rentAirCooler.GetByItemId(itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _rentAirCooler.GetByAF(AF);

            var viewModel = new MiscellaneousTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.MiscellaneousTransactionDto = Mapper.Map<MiscellaneousTransactionDto>(transaction);
            viewModel.Header = transaction.MiscellaneousItem.Miscellaneous.Site.Header;

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

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var miscellaneousTransactionDto = new MiscellaneousTransactionDto();

            var item = _item.GetById(itemId);
            if (AF == null)
            {
                miscellaneousTransactionDto.ApplicantDtoId = applicantId;
                miscellaneousTransactionDto.MiscellaneousItemDto = Mapper.Map<MiscellaneousItemDto>(item);
                miscellaneousTransactionDto.MiscellaneousItemDtoId = itemId;
                miscellaneousTransactionDto.Amount = _item.GetPrice(item);
            }
            else
            {
                miscellaneousTransactionDto = Mapper.Map<MiscellaneousTransactionDto>(_rentAirCooler.GetByAF(AF));
            }

            return View(miscellaneousTransactionDto);
        }

        public ActionResult Save(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            var miscellaneousTransaction = Mapper.Map<Core.Domain.MiscellaneousTransaction>(miscellaneousTransactionDto);
            if ((miscellaneousTransactionDto.AF == null && _rentAirCooler.Add(miscellaneousTransaction)) ||
                (miscellaneousTransactionDto.AF != null && _rentAirCooler.Change(miscellaneousTransaction.AF, miscellaneousTransaction)))
            {
                return RedirectToAction("Index", new { itemId = miscellaneousTransaction.MiscellaneousItemId, applicantId = miscellaneousTransaction.ApplicantId });
            }

            return View("Form", miscellaneousTransactionDto);
        }


        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            var status = _rentAirCooler.Remove(AF);

            return RedirectToAction("Index", new
            {
                itemId,
                applicantId
            });
        }

        public ActionResult Invoices(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Miscellaneous" });
        }

    }
}