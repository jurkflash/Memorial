using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;
using Memorial.Lib;

namespace Memorial.Controllers
{
    public class CremationController : Controller
    {
        private readonly ICremation _cremation;

        public CremationController(ICremation cremation)
        {
            _cremation = cremation;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var viewModel = new CremationIndexesViewModel()
            {
                CremationDtos = _cremation.DtosGetBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int cremationId, int applicantId)
        {
            var viewModel = new CremationItemsViewModel()
            {
                CremationItemDtos = _cremation.ItemDtosGetByCremation(cremationId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Item(int cremationItemId, int applicantId)
        {
            return RedirectToAction("Index", "CremationTransactions", new { cremationItemId = cremationItemId, applicantId = applicantId });
        }
    }
}