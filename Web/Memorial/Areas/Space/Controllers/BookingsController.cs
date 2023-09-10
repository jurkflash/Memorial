using System;
using System.Web.Mvc;
using Memorial.Lib.Space;
using Memorial.Lib.Deceased;
using Memorial.Lib.FuneralCompany;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

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

        public ActionResult Index(int itemId, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var item = _item.GetById(itemId);
            var viewModel = new SpaceItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                SpaceItemDto = Mapper.Map<SpaceItemDto>(item),
                SpaceName = item.Space.Name,
                SpaceTransactionDtos = Mapper.Map<IEnumerable<SpaceTransactionDto>>(_booking.GetByItemId(itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _booking.GetByAF(AF);
            var item = _item.GetById(transaction.SpaceItemId);
            
            var viewModel = new SpaceTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = item.SubProductService.Name;
            viewModel.SpaceDto = Mapper.Map<SpaceDto>(transaction.SpaceItem.Space);
            viewModel.SpaceTransactionDto = Mapper.Map<SpaceTransactionDto>(transaction);
            viewModel.TotalDays = (int)Math.Ceiling(((DateTime)transaction.ToDate - (DateTime)transaction.FromDate).TotalDays);
            viewModel.TotalAmount = _booking.GetTotalAmount(transaction);
            viewModel.ApplicantId = transaction.ApplicantId;
            viewModel.DeceasedId = transaction.DeceasedId;
            viewModel.Header = transaction.SpaceItem.Space.Site.Header;

            return View(item.FormView, viewModel);
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
            var viewModel = new SpaceTransactionsFormViewModel()
            {
                FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll()),
                DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(applicantId))
            };

            var item = _item.GetById(itemId);
            if (AF == null)
            {
                var spaceTransactionDto = new SpaceTransactionDto();
                spaceTransactionDto.ApplicantDtoId = applicantId;
                spaceTransactionDto.SpaceItemDto = Mapper.Map<SpaceItemDto>(item);
                spaceTransactionDto.SpaceItemDtoId = itemId;
                spaceTransactionDto.BasePrice = _item.GetPrice(item);
                spaceTransactionDto.Amount = _item.GetPrice(item);
                viewModel.SpaceTransactionDto = spaceTransactionDto;
            }
            else
            {
                viewModel.SpaceTransactionDto = Mapper.Map<SpaceTransactionDto>(_booking.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult FormForResubmit(SpaceTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll());
            viewModel.DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(viewModel.SpaceTransactionDto.ApplicantDtoId));

            return View("Form", viewModel);
        }

        public ActionResult Save(SpaceTransactionsFormViewModel viewModel)
        {
            var spaceTransaction = Mapper.Map<Core.Domain.SpaceTransaction>(viewModel.SpaceTransactionDto);
            if((spaceTransaction.AF == null &&
                !_booking.IsAvailable(spaceTransaction.SpaceItemId, (DateTime)spaceTransaction.FromDate, (DateTime)spaceTransaction.ToDate)) ||
                (spaceTransaction.AF != null &&
                !_booking.IsAvailable(spaceTransaction.AF, (DateTime)spaceTransaction.FromDate, (DateTime)spaceTransaction.ToDate)))
            {
                ModelState.AddModelError("SpaceTransactionDto.FromDate", "Not available");
                ModelState.AddModelError("SpaceTransactionDto.ToDate", "Not available");
            }


            if ((spaceTransaction.AF == null && _booking.Add(spaceTransaction)) ||
                (spaceTransaction.AF != null && _booking.Change(spaceTransaction.AF, spaceTransaction)))
            {
                return RedirectToAction("Index", new { itemId = viewModel.SpaceTransactionDto.SpaceItemDtoId, applicantId = viewModel.SpaceTransactionDto.ApplicantDtoId });
            }

            return FormForResubmit(viewModel);

        }

        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            var status = _booking.Remove(AF);

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