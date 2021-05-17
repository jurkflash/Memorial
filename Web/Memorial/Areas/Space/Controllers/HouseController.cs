using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Space;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;

namespace Memorial.Areas.Space.Controllers
{
    public class HouseController : Controller
    {
        private readonly ISpace _space;
        private readonly IItem _item;
        private readonly IHouse _house;
        private readonly IApplicant _applicant;
        private readonly Lib.Invoice.ISpace _invoice;

        public HouseController(
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

        public ActionResult Index(int itemId, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var viewModel = new SpaceItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                SpaceItemId = itemId,
                SpaceTransactionDtos = _house.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != 0
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _house.SetHouse(AF);

            var viewModel = new SpaceTransactionsInfoViewModel()
            {
                SpaceTransactionDto = _house.GetTransactionDto(),
                ItemName = _house.GetItemName()
            };

            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var spaceTransactionDto = new SpaceTransactionDto();

            _item.SetItem(itemId);

            if (AF == null)
            {
                spaceTransactionDto.ApplicantId = applicantId;
                spaceTransactionDto.SpaceItemId = itemId;
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
                return RedirectToAction("Index", new { itemId = spaceTransactionDto.SpaceItemId, applicantId = spaceTransactionDto.ApplicantId });
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