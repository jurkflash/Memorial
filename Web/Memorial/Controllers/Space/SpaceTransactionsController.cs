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
    public class SpaceTransactionsController : Controller
    {
        private readonly ISpaceTransaction _spaceTransaction;
        private readonly ISpaceItem _spaceItem;

        public SpaceTransactionsController(ISpaceTransaction spaceTransaction, ISpaceItem spaceItem)
        {
            _spaceTransaction = spaceTransaction;
            _spaceItem = spaceItem;

        }
        public ActionResult Index(int spaceItemId, int applicantId)
        {
            var viewModel = new SpaceItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                SpaceItemId = spaceItemId,
                SpaceTransactionDtos = _spaceTransaction.GetByItemAndApplicantDtos(applicantId, spaceItemId)
            };
            return View(viewModel);
        }

        public ActionResult New(int spaceItemId, int applicantId)
        {
            var spaceTransactionDto = new SpaceTransactionDto();
            spaceTransactionDto.ApplicantId = applicantId;
            spaceTransactionDto.SpaceItemId = spaceItemId;
            return View("Form", spaceTransactionDto);
        }

        public ActionResult Save(SpaceTransactionDto spaceTransactionDto)
        {
            if(_spaceTransaction.CreateNewTransaction(spaceTransactionDto))
            {
                return RedirectToAction("Index", new { spaceItemId = spaceTransactionDto.SpaceItemId, applicantId = spaceTransactionDto.ApplicantId });
            }
            else
            {
                return View("Form", spaceTransactionDto);
            }
        }

        public ActionResult Info(string AF)
        {
            return View(_spaceTransaction.GetTransactionDto(AF));
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, masterCatalog = MasterCatalog.Space });
        }
    }
}