using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Applicant;
using Memorial.Lib.Urn;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;

namespace Memorial.Areas.Urn.Controllers
{
    public class UrnsController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IUrn _urn;
        private readonly IItem _item;
        private readonly ISite _site;

        public UrnsController(
            IApplicant applicant,
            IUrn urn,
            IItem item,
            ISite site)
        {
            _applicant = applicant;
            _urn = urn;
            _item = item;
            _site = site;
        }

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new UrnIndexesViewModel()
            {
                UrnDtos = _urn.GetUrnDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int urnId, int applicantId)
        {
            var viewModel = new UrnItemsViewModel()
            {
                UrnItemDtos = _item.GetItemDtosByUrn(urnId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

    }
}