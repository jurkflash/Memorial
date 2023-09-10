using System.Web.Mvc;
using Memorial.Lib.Miscellaneous;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.Miscellaneous.Controllers
{
    public class DonationsController : Controller
    {
        private readonly IMiscellaneous _miscellaneous;
        private readonly IItem _item;
        private readonly IDonation _donation;

        public DonationsController(
            IMiscellaneous miscellaneous,
            IItem item,
            IDonation donation
            )
        {
            _miscellaneous = miscellaneous;
            _item = item;
            _donation = donation;
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
                MiscellaneousTransactionDtos = Mapper.Map<IEnumerable<MiscellaneousTransactionDto>>(_donation.GetByItemId(itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
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
                miscellaneousTransactionDto = Mapper.Map<MiscellaneousTransactionDto>(_donation.GetByAF(AF));
            }

            return View(miscellaneousTransactionDto);
        }

        public ActionResult Save(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            var miscellaneousTransaction = Mapper.Map<Core.Domain.MiscellaneousTransaction>(miscellaneousTransactionDto);
            if ((miscellaneousTransactionDto.AF == null && _donation.Add(miscellaneousTransaction)) ||
                (miscellaneousTransactionDto.AF != null && _donation.Change(miscellaneousTransaction.AF, miscellaneousTransaction)))
            {
                return RedirectToAction("Index", new { itemId = miscellaneousTransaction.MiscellaneousItemId, applicantId = miscellaneousTransaction.ApplicantId });
            }

            return View("Form", miscellaneousTransactionDto);
        }


        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            var status = _donation.Remove(AF);

            return RedirectToAction("Index", new
            {
                itemId,
                applicantId
            });
        }

        public ActionResult Receipts(string AF)
        {
            return RedirectToAction("Index", "NonOrderReceipts", new { AF = AF, area = "Miscellaneous" });
        }

    }
}