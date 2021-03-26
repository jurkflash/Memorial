using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using Memorial.Lib;
using AutoMapper;

namespace Memorial.Controllers.Cremation
{
    public class CremationTransactionsController : Controller
    {
        private readonly ICremation _cremation;
        private readonly IDeceased _deceased;
        private readonly IFuneralCo _funeralCo;

        public CremationTransactionsController(ICremation cremation, IDeceased deceased, IFuneralCo funeralCo)
        {
            _cremation = cremation;
            _deceased = deceased;
            _funeralCo = funeralCo;
        }

        public ActionResult Index(int cremationItemId, int applicantId)
        {
            var viewModel = new CremationOrdersViewModel()
            {
                ApplicantId = applicantId,
                CremationItemId = cremationItemId,
                CremationTransactionDtos = _cremation.TransactionDtosGetByItemAndApplicant(cremationItemId, applicantId)
            };
            return View(viewModel);
        }

        public ActionResult New(int cremationItemId, int applicantId)
        {
            var cremationTransactionDto = new CremationTransactionDto();
            cremationTransactionDto.ApplicantId = applicantId;
            cremationTransactionDto.CremationItemId = cremationItemId;
            var viewModel = new CremationOrderFormViewModel()
            {
                FuneralCompanyDtos = _funeralCo.GetAll(),
                DeceasedBriefDtos = _deceased.BriefDtosGetByApplicant(applicantId),
                CremationTransactionDto = cremationTransactionDto
            };
            return View("Form", viewModel);
        }

        public ActionResult Save(CremationOrderFormViewModel viewModel)
        {
            if(!_cremation.CreateNewTransaction(viewModel.CremationTransactionDto))
            {
                viewModel.FuneralCompanyDtos = _funeralCo.GetAll();
                viewModel.DeceasedBriefDtos = _deceased.BriefDtosGetByApplicant(viewModel.CremationTransactionDto.ApplicantId);
                return View("Form", viewModel);
            }

            return RedirectToAction("Index", new { cremationItemId = viewModel.CremationTransactionDto.CremationItemId, applicantId = viewModel.CremationTransactionDto.ApplicantId });
        }

        public ActionResult Info(string AF)
        {
            var cremationTransactionDto = _cremation.GetTransactionDto(AF);
            return View(cremationTransactionDto);
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, masterCatalog = MasterCatalog.Cremation });
        }
    }
}