using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;

namespace Memorial.Controllers
{
    public class UrnController : Controller
    {
        private readonly IUrn _urn;

        public UrnController(IUrn urn)
        {
            _urn = urn;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var viewModel = new UrnIndexesViewModel()
            {
                UrnDtos = _urn.DtosGetBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int urnId, int applicantId)
        {
            var viewModel = new UrnItemsViewModel()
            {
                UrnItemDtos = _urn.ItemDtosGetByUrn(urnId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Transactions(int urnItemId, int applicantId)
        {
            var viewModel = new UrnItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                UrnItemId = urnItemId,
                OrderFlag = _urn.IsOrderFlag(urnItemId),
                UrnTransactionDtos = _urn.TransactionDtosGetByItemAndApplicant(urnItemId, applicantId)
                                                                        .OrderByDescending(mt => mt.CreateDate)
            };
            return View(viewModel);
        }

        public ActionResult New(int urnItemId, int applicantId)
        {
            var urnTransactionDto = new UrnTransactionDto();
            urnTransactionDto.ApplicantId = applicantId;
            urnTransactionDto.UrnItemId = urnItemId;
            return View("Form", urnTransactionDto);
        }

        public ActionResult Save(UrnTransactionDto urnTransactionDto)
        {
            if (_urn.CreateNewTransaction(urnTransactionDto))
                return RedirectToAction("Transactions", new { urnItemId = urnTransactionDto.UrnItemId, applicantId = urnTransactionDto.ApplicantId });
            else
                return View("Form", urnTransactionDto);
        }

        public ActionResult Info(string AF)
        {
            return View(_urn.GetTransactionDto(AF));
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, masterCatalog = MasterCatalog.Urn });
        }

        public ActionResult Receipt(string AF)
        {
            return RedirectToAction("Index", "NonOrderReceipts", new { AF = AF, MasterCatalog = MasterCatalog.Urn });
        }

        public ActionResult Delete(string AF, int urnItemId, int applicantId)
        {
            _urn.Delete(AF);

            return RedirectToAction("Index", new { urnItemId = urnItemId, applicantId = applicantId });
        }
    }
}