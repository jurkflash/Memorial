using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Miscellaneous;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;

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

            _item.SetItem(itemId);

            var viewModel = new MiscellaneousItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                MiscellaneousItemDto = _item.GetItemDto(),
                MiscellaneousTransactionDtos = _rentAirCooler.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _rentAirCooler.SetTransaction(AF);
            _miscellaneous.SetMiscellaneous(_rentAirCooler.GetTransactionDto().MiscellaneousItemDto.MiscellaneousDtoId);

            var viewModel = new MiscellaneousTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.MiscellaneousTransactionDto = _rentAirCooler.GetTransactionDto();
            viewModel.Header = _miscellaneous.GetMiscellaneous().Site.Header;

            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            return report;
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var miscellaneousTransactionDto = new MiscellaneousTransactionDto();

            _item.SetItem(itemId);
            _miscellaneous.SetMiscellaneous(_item.GetMiscellaneousId());

            if (AF == null)
            {
                miscellaneousTransactionDto.ApplicantDtoId = applicantId;
                miscellaneousTransactionDto.MiscellaneousItemDto = _item.GetItemDto();
                miscellaneousTransactionDto.MiscellaneousItemDtoId = itemId;
                miscellaneousTransactionDto.Amount = _item.GetPrice();
            }
            else
            {
                miscellaneousTransactionDto = _rentAirCooler.GetTransactionDto(AF);
            }

            return View(miscellaneousTransactionDto);
        }

        public ActionResult Save(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            if (miscellaneousTransactionDto.AF == null)
            {
                if (!_rentAirCooler.Create(miscellaneousTransactionDto))
                {
                    return View("Form", miscellaneousTransactionDto);
                }
            }
            else
            {
                if (!_rentAirCooler.Update(miscellaneousTransactionDto))
                {
                    return View("Form", miscellaneousTransactionDto);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = miscellaneousTransactionDto.MiscellaneousItemDtoId,
                applicantId = miscellaneousTransactionDto.ApplicantDtoId
            });
        }


        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _rentAirCooler.SetTransaction(AF);
            _rentAirCooler.Delete();

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