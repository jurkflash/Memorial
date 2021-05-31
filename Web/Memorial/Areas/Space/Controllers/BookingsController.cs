using System;
using System.Web.Mvc;
using Memorial.Lib.Space;
using Memorial.Lib.Deceased;
using Memorial.Lib.FuneralCompany;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Space.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IDeceased _deceased;
        private readonly ISpace _space;
        private readonly IFuneralCompany _funeralCompany;
        private readonly IItem _item;
        private readonly IBooking _booking;

        public BookingsController(
            IItem item,
            ISpace space,
            IDeceased deceased,
            IFuneralCompany funeralCompany,
            IBooking booking
            )
        {
            _item = item;
            _space = space;
            _deceased = deceased;
            _funeralCompany = funeralCompany;
            _booking = booking;
        }

        public ActionResult Index(int itemId, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _item.SetItem(itemId);

            var viewModel = new SpaceItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                SpaceItemId = itemId,
                SpaceItemName = _item.GetName(),
                SpaceName = _item.GetItem().Space.Name,
                SpaceTransactionDtos = _booking.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != 0
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _booking.SetTransaction(AF);
            _item.SetItem(_booking.GetTransactionSpaceItemId());
            _space.SetSpace(_item.GetItem().SpaceId);

            var viewModel = new SpaceTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _booking.GetItemName();
            viewModel.SpaceDto = _space.GetSpaceDto();
            viewModel.SpaceTransactionDto = _booking.GetTransactionDto();
            viewModel.TotalDays = (int)Math.Ceiling(((DateTime)_booking.GetTransactionDto().ToDate - (DateTime)_booking.GetTransactionDto().FromDate).TotalDays);
            viewModel.TotalAmount = _booking.GetTransactionTotalAmount();
            viewModel.ApplicantId = _booking.GetTransactionApplicantId();
            viewModel.DeceasedId = _booking.GetTransactionDeceasedId();
            viewModel.Header = _booking.GetSiteHeader();

            return View(_item.GetItem().FormView, viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            return report;
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new SpaceTransactionsFormViewModel()
            {
                FuneralCompanyDtos = _funeralCompany.GetFuneralCompanyDtos(),
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            _item.SetItem(itemId);

            if (AF == null)
            {
                var spaceTransactionDto = new SpaceTransactionDto();
                spaceTransactionDto.ApplicantId = applicantId;
                spaceTransactionDto.SpaceItemId = itemId;
                spaceTransactionDto.BasePrice = _item.GetPrice();
                spaceTransactionDto.Amount = _item.GetPrice();
                viewModel.SpaceTransactionDto = spaceTransactionDto;
            }
            else
            {
                viewModel.SpaceTransactionDto = _booking.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult FormForResubmit(SpaceTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = _funeralCompany.GetFuneralCompanyDtos();
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.SpaceTransactionDto.ApplicantId);

            return View("Form", viewModel);
        }

        public ActionResult Save(SpaceTransactionsFormViewModel viewModel)
        {
            if((viewModel.SpaceTransactionDto.AF == null &&
                !_booking.IsAvailable(viewModel.SpaceTransactionDto.SpaceItemId, (DateTime)viewModel.SpaceTransactionDto.FromDate, (DateTime)viewModel.SpaceTransactionDto.ToDate)) ||
                (viewModel.SpaceTransactionDto.AF != null &&
                !_booking.IsAvailable(viewModel.SpaceTransactionDto.AF, (DateTime)viewModel.SpaceTransactionDto.FromDate, (DateTime)viewModel.SpaceTransactionDto.ToDate)))
            {
                ModelState.AddModelError("SpaceTransactionDto.FromDate", "Not available");
                ModelState.AddModelError("SpaceTransactionDto.ToDate", "Not available");
            }


            if ((viewModel.SpaceTransactionDto.AF == null && _booking.Create(viewModel.SpaceTransactionDto)) ||
                (viewModel.SpaceTransactionDto.AF != null && _booking.Update(viewModel.SpaceTransactionDto)))
            {
                return RedirectToAction("Index", new { itemId = viewModel.SpaceTransactionDto.SpaceItemId, applicantId = viewModel.SpaceTransactionDto.ApplicantId });
            }

            return FormForResubmit(viewModel);

        }

        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _booking.SetTransaction(AF);
            _booking.Delete();

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