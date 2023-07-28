using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Space;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;
using System.Collections.Generic;

namespace Memorial.Areas.Space.Controllers
{
    public class ElectricController : Controller
    {
        private readonly ISpace _space;
        private readonly IItem _item;
        private readonly IElectric _electric;
        private readonly IApplicant _applicant;
        private Lib.Invoice.ISpace _invoice;

        public ElectricController(
            ISpace space,
            IItem item,
            IApplicant applicant,
            IElectric electric,
            Lib.Invoice.ISpace invoice
            )
        {
            _space = space;
            _item = item;
            _applicant = applicant;
            _electric = electric;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _item.SetItem(itemId);

            var viewModel = new SpaceItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                SpaceItemDto = _item.GetItemDto(),
                SpaceName = _item.GetItem().Space.Name,
                SpaceTransactionDtos = _electric.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _electric.SetTransaction(AF);
            _item.SetItem(_electric.GetTransactionSpaceItemId());
            _space.SetSpace(_item.GetItem().SpaceId);

            var viewModel = new SpaceTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _electric.GetItemName();
            viewModel.SpaceDto = _space.GetSpaceDto();
            viewModel.SpaceTransactionDto = _electric.GetTransactionDto();
            viewModel.ApplicantId = _electric.GetTransactionApplicantId();
            viewModel.DeceasedId = _electric.GetTransactionDeceasedId();
            viewModel.Header = _electric.GetSiteHeader();

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
            var spaceTransactionDto = new SpaceTransactionDto();

            _item.SetItem(itemId);

            if (AF == null)
            {
                spaceTransactionDto.ApplicantDtoId = applicantId;
                spaceTransactionDto.SpaceItemDtoId = itemId;
                spaceTransactionDto.SpaceItemDto = _item.GetItemDto();
                spaceTransactionDto.Amount = _item.GetPrice();
            }
            else
            {
                spaceTransactionDto = _electric.GetTransactionDto(AF);
            }

            return View(spaceTransactionDto);
        }

        public ActionResult Save(SpaceTransactionDto spaceTransactionDto)
        {
            if ((spaceTransactionDto.AF == null && _electric.Create(spaceTransactionDto)) ||
                (spaceTransactionDto.AF != null && _electric.Update(spaceTransactionDto)))
            {
                return RedirectToAction("Index", new { itemId = spaceTransactionDto.SpaceItemDtoId, applicantId = spaceTransactionDto.ApplicantDtoId });
            }

            return View("Form", spaceTransactionDto);
        }

        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _electric.SetTransaction(AF);
            _electric.Delete();

            return RedirectToAction("Index", new
            {
                itemId,
                applicantId
            });
        }

        public ActionResult Invoices(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Space" });
        }

    }
}