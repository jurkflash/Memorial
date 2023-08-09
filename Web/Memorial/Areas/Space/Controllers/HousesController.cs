using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Space;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;
using System.Collections.Generic;
using AutoMapper;
using System;

namespace Memorial.Areas.Space.Controllers
{
    public class HousesController : Controller
    {
        private readonly ISpace _space;
        private readonly IItem _item;
        private readonly IHouse _house;
        private readonly IApplicant _applicant;

        public HousesController(
            ISpace space,
            IItem item,
            IApplicant applicant,
            IHouse house
            )
        {
            _space = space;
            _item = item;
            _applicant = applicant;
            _house = house;
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
                SpaceTransactionDtos = Mapper.Map<IEnumerable<SpaceTransactionDto>>(_house.GetByItemId(itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _house.GetByAF(AF);
            var item = _item.GetById(transaction.SpaceItemId);

            var viewModel = new SpaceTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = item.SubProductService.Name;
            viewModel.SpaceDto = Mapper.Map<SpaceDto>(transaction.SpaceItem.Space);
            viewModel.SpaceTransactionDto = Mapper.Map<SpaceTransactionDto>(transaction);
            viewModel.ApplicantId = transaction.ApplicantId;
            viewModel.DeceasedId = transaction.DeceasedId;
            viewModel.Header = transaction.SpaceItem.Space.Site.Header;

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

            var item = _item.GetById(itemId);
            if (AF == null)
            {
                spaceTransactionDto.ApplicantDtoId = applicantId;
                spaceTransactionDto.SpaceItemDto = Mapper.Map<SpaceItemDto>(item);
                spaceTransactionDto.SpaceItemDtoId = itemId;
                spaceTransactionDto.Amount = _item.GetPrice(item);
            }
            else
            {
                spaceTransactionDto = Mapper.Map<SpaceTransactionDto>(_house.GetByAF(AF));
            }

            return View(spaceTransactionDto);
        }

        public ActionResult Save(SpaceTransactionDto spaceTransactionDto)
        {
            var spaceTransaction = Mapper.Map<Core.Domain.SpaceTransaction>(spaceTransactionDto);
            if ((spaceTransactionDto.AF == null && _house.Add(spaceTransaction)) ||
                (spaceTransactionDto.AF != null && _house.Change(spaceTransaction.AF, spaceTransaction)))
            {
                return RedirectToAction("Index", new { itemId = spaceTransaction.SpaceItemId, applicantId = spaceTransaction.ApplicantId });
            }

            return View("Form", spaceTransactionDto);
        }

        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            var status = _house.Remove(AF);

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