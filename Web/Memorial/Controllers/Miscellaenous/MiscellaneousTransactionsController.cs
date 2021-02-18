using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Controllers
{
    public class MiscellaneousTransactionsController : Controller
    {
        private readonly IMiscellaneousTransaction _miscellaneousTransaction;
        private readonly IMiscellaneousItem _miscellaneousItem;

        public MiscellaneousTransactionsController(IMiscellaneousTransaction miscellaneousTransaction, IMiscellaneousItem miscellaneousItem)
        {
            _miscellaneousTransaction = miscellaneousTransaction;
            _miscellaneousItem = miscellaneousItem;
        }

        public ActionResult Index(int miscellaneousItemId, int applicantId)
        {
            var viewModel = new MiscellaneousItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                MiscellaneousItemId = miscellaneousItemId,
                OrderFlag = _miscellaneousItem.IsOrderFlag(miscellaneousItemId),
                MiscellaneousTransactionDtos = _miscellaneousTransaction.DtosGetByItemAndApplicant(miscellaneousItemId, applicantId)
                                                                        .OrderByDescending(mt=>mt.CreateDate)
            };
            return View(viewModel);
        }

        public ActionResult New(int miscellaneousItemId, int applicantId)
        {
            var miscellaneousTransactionDto = new MiscellaneousTransactionDto();
            miscellaneousTransactionDto.ApplicantId = applicantId;
            miscellaneousTransactionDto.MiscellaneousItemId = miscellaneousItemId;
            return View("Form", miscellaneousTransactionDto);
        }

        public ActionResult Save(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            if (_miscellaneousTransaction.CreateNewTransaction(miscellaneousTransactionDto))
                return RedirectToAction("Index", new { miscellaneousItemId = miscellaneousTransactionDto.MiscellaneousItemId, applicantId = miscellaneousTransactionDto.ApplicantId });
            else
                return View("Form", miscellaneousTransactionDto);
        }

        public ActionResult Info(string AF)
        {
            return View(_miscellaneousTransaction.GetTransactionDto(AF));
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, masterCatalog = MasterCatalog.Miscellaneous });
        }

        public ActionResult Receipt(string AF)
        {
            return RedirectToAction("Index", "NonOrderReceipts", new { AF = AF, MasterCatalog = MasterCatalog.Miscellaneous });
        }

        public ActionResult Delete(string AF, int miscellaneousItemId, int applicantId)
        {
            _miscellaneousTransaction.Delete(AF);

            return RedirectToAction("Index", new { miscellaneousItemId = miscellaneousItemId, applicantId = applicantId });
        }
    }
}