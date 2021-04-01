using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Applicant;
using Memorial.Lib.Cremation;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;

namespace Memorial.Areas.Cremation.Controllers
{
    public class CremationController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly ICremation _cremation;
        private readonly IItem _item;
        private readonly ISite _site;

        public CremationController(
            IApplicant applicant,
            ICremation cremation,
            IItem item,
            ISite site)
        {
            _applicant = applicant;
            _cremation = cremation;
            _item = item;
            _site = site;
        }

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new CremationIndexesViewModel()
            {
                CremationDtos = _cremation.GetCremationDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int cremationId, int applicantId)
        {
            var viewModel = new CremationItemsViewModel()
            {
                CremationItemDtos = _item.GetItemDtosByCremation(cremationId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

    }
}