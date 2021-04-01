using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Space;
using Memorial.Lib.Applicant;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Space.Controllers
{
    public class ChairController : Controller
    {
        private readonly ISpace _space;
        private readonly IItem _item;
        private readonly IChair _chair;
        private readonly IApplicant _applicant;
        private readonly Lib.Invoice.ISpace _invoice;

        public ChairController(
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

        public ActionResult Index(int itemId, int applicantId)
        {
            var viewModel = new SpaceItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                SpaceItemId = itemId,
                SpaceTransactionDtos = _chair.GetTransactionDtosByItemId(itemId),
                AllowNew = applicantId != 0
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _chair.SetChair(AF);

            var viewModel = new SpaceTransactionsInfoViewModel()
            {
                SpaceTransactionDto = _chair.GetTransactionDto(),
                ItemName = _chair.GetItemName()
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
                spaceTransactionDto = _chair.GetTransactionDto(AF);
            }

            return View(spaceTransactionDto);
        }

        public ActionResult Save(SpaceTransactionDto spaceTransactionDto)
        {
            if ((spaceTransactionDto.AF == null && _chair.Create(spaceTransactionDto)) ||
                (spaceTransactionDto.AF != null && _chair.Update(spaceTransactionDto)))
            {
                return RedirectToAction("Index", new { itemId = spaceTransactionDto.SpaceItemId, applicantId = spaceTransactionDto.ApplicantId });
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
            return RedirectToAction("Index", "SpaceInvoices", new { AF = AF });
        }

    }
}