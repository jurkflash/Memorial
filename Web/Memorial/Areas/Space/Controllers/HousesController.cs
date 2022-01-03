using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Space;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;

namespace Memorial.Areas.Space.Controllers
{
    public class HousesController : Controller
    {
        private readonly ISpace _space;
        private readonly IItem _item;
        private readonly IHouse _house;
        private readonly IApplicant _applicant;
        private readonly Lib.Invoice.ISpace _invoice;

        public HousesController(
            ISpace space,
            IItem item,
            IApplicant applicant,
            IHouse house,
            Lib.Invoice.ISpace invoice
            )
        {
            _space = space;
            _item = item;
            _applicant = applicant;
            _house = house;
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
                SpaceItemId = itemId,
                SpaceItemName = _item.GetName(),
                SpaceName = _item.GetItem().Space.Name,
                SpaceTransactionDtos = _house.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _house.SetTransaction(AF);
            _item.SetItem(_house.GetTransactionSpaceItemId());
            _space.SetSpace(_item.GetItem().SpaceId);

            var viewModel = new SpaceTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _house.GetItemName();
            viewModel.SpaceDto = _space.GetSpaceDto();
            viewModel.SpaceTransactionDto = _house.GetTransactionDto();
            viewModel.ApplicantId = _house.GetTransactionApplicantId();
            viewModel.DeceasedId = _house.GetTransactionDeceasedId();
            viewModel.Header = _house.GetSiteHeader();

            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
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
                spaceTransactionDto.Amount = _item.GetPrice();
            }
            else
            {
                spaceTransactionDto = _house.GetTransactionDto(AF);
            }

            return View(spaceTransactionDto);
        }

        public ActionResult Save(SpaceTransactionDto spaceTransactionDto)
        {
            if ((spaceTransactionDto.AF == null && _house.Create(spaceTransactionDto)) ||
                (spaceTransactionDto.AF != null && _house.Update(spaceTransactionDto)))
            {
                return RedirectToAction("Index", new { itemId = spaceTransactionDto.SpaceItemDtoId, applicantId = spaceTransactionDto.ApplicantDtoId });
            }

            return View("Form", spaceTransactionDto);
        }

        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _house.SetTransaction(AF);
            _house.Delete();

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