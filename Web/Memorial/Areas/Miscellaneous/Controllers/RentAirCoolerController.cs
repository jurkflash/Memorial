﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Miscellaneous;
using Memorial.Lib.Applicant;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Miscellaneous.Controllers
{
    public class RentAirCoolerController : Controller
    {
        private readonly IMiscellaneous _miscellaneous;
        private readonly IItem _item;
        private readonly IRentAirCooler _rentAirCooler;

        public RentAirCoolerController(
            IMiscellaneous miscellaneous,
            IItem item,
            IRentAirCooler rentAirCooler
            )
        {
            _miscellaneous = miscellaneous;
            _item = item;
            _rentAirCooler = rentAirCooler;
        }

        public ActionResult Index(int itemId, int applicantId)
        {
            var viewModel = new MiscellaneousItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                MiscellaneousItemId = itemId,
                MiscellaneousTransactionDtos = _rentAirCooler.GetTransactionDtosByItemId(itemId),
                AllowNew = applicantId != 0
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            return View(_rentAirCooler.GetTransactionDto(AF));
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var miscellaneousTransactionDto = new MiscellaneousTransactionDto();

            _item.SetItem(itemId);
            _miscellaneous.SetMiscellaneous(_item.GetMiscellaneousId());

            if (AF == null)
            {
                miscellaneousTransactionDto.ApplicantDtoId = applicantId;
                miscellaneousTransactionDto.MiscellaneousItemDtoId = itemId;
                miscellaneousTransactionDto.Amount = _item.GetPrice();
            }
            else
            {
                miscellaneousTransactionDto = _rentAirCooler.GetTransactionDto(AF);
            }

            return View(miscellaneousTransactionDto);
        }

        public ActionResult Save(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            if (miscellaneousTransactionDto.AF == null)
            {
                if (!_rentAirCooler.Create(miscellaneousTransactionDto))
                {
                    return View("Form", miscellaneousTransactionDto);
                }
            }
            else
            {
                if (!_rentAirCooler.Update(miscellaneousTransactionDto))
                {
                    return View("Form", miscellaneousTransactionDto);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = miscellaneousTransactionDto.MiscellaneousItemDtoId,
                applicantId = miscellaneousTransactionDto.ApplicantDtoId
            });
        }


        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _rentAirCooler.SetTransaction(AF);
            _rentAirCooler.Delete();

            return RedirectToAction("Index", new
            {
                itemId,
                applicantId
            });
        }

        public ActionResult Invoices(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Miscellaneous" });
        }

    }
}