﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Space;
using Memorial.Lib.Deceased;
using Memorial.Lib.FuneralCompany;
using Memorial.Lib.Applicant;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Space.Controllers
{
    public class BookingController : Controller
    {
        private readonly ISpace _space;
        private readonly IDeceased _deceased;
        private readonly IFuneralCompany _funeralCompany;
        private readonly IItem _item;
        private readonly IBooking _booking;
        private readonly IApplicant _applicant;
        private readonly Lib.Invoice.ISpace _invoice;

        public BookingController(
            ISpace space,
            IItem item,
            IApplicant applicant,
            IDeceased deceased,
            IFuneralCompany funeralCompany,
            IBooking booking,
            Lib.Invoice.ISpace invoice
            )
        {
            _space = space;
            _item = item;
            _applicant = applicant;
            _deceased = deceased;
            _funeralCompany = funeralCompany;
            _booking = booking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int applicantId)
        {
            var viewModel = new SpaceItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                SpaceItemId = itemId,
                SpaceTransactionDtos = _booking.GetTransactionDtosByItemId(itemId),
                AllowNew = applicantId != 0
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _booking.SetBooking(AF);

            var viewModel = new SpaceTransactionsInfoViewModel()
            {
                SpaceTransactionDto = _booking.GetTransactionDto(),
                ItemName = _booking.GetItemName()
            };

            return View(viewModel);
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