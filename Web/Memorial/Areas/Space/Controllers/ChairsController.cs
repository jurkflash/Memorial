using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Space;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;

namespace Memorial.Areas.Space.Controllers
{
    public class ChairsController : Controller
    {
        private readonly ISpace _space;
        private readonly IItem _item;
        private readonly IChair _chair;
        private readonly IApplicant _applicant;
        private readonly Lib.Invoice.ISpace _invoice;

        public ChairsController(
            ISpace space,
            IItem item,
            IApplicant applicant,
            IChair chair,
            Lib.Invoice.ISpace invoice
            )
        {
            _space = space;
            _item = item;
            _applicant = applicant;
            _chair = chair;
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
                SpaceTransactionDtos = _chair.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _chair.SetTransaction(AF);
            _item.SetItem(_chair.GetTransactionSpaceItemId());
            _space.SetSpace(_item.GetItem().SpaceId);

            var viewModel = new SpaceTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _chair.GetItemName();
            viewModel.SpaceDto = _space.GetSpaceDto();
            viewModel.SpaceTransactionDto = _chair.GetTransactionDto();
            viewModel.ApplicantId = _chair.GetTransactionApplicantId();
            viewModel.DeceasedId = _chair.GetTransactionDeceasedId();
            viewModel.Header = _chair.GetSiteHeader();

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
                spaceTransactionDto = _chair.GetTransactionDto(AF);
            }

            return View(spaceTransactionDto);
        }

        public ActionResult Save(SpaceTransactionDto spaceTransactionDto)
        {
            if ((spaceTransactionDto.AF == null && _chair.Create(spaceTransactionDto)) ||
                (spaceTransactionDto.AF != null && _chair.Update(spaceTransactionDto)))
            {
                return RedirectToAction("Index", new { itemId = spaceTransactionDto.SpaceItemDtoId, applicantId = spaceTransactionDto.ApplicantDtoId });
            }

            return View("Form", spaceTransactionDto);
        }

        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _chair.SetTransaction(AF);
            _chair.Delete();

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