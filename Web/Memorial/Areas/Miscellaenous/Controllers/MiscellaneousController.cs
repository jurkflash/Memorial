using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Applicant;
using Memorial.Lib.Miscellaneous;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;

namespace Memorial.Areas.Miscellaenous.Controllers
{
    public class MiscellaneousController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IMiscellaneous _miscellaneous;
        private readonly IItem _item;
        private readonly ISite _site;

        public MiscellaneousController(
            IApplicant applicant,
            IMiscellaneous miscellaneous,
            IItem item,
            ISite site)
        {
            _applicant = applicant;
            _miscellaneous = miscellaneous;
            _item = item;
            _site = site;
        }

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new MiscellaneousIndexesViewModel()
            {
                MiscellaneousDtos = _miscellaneous.GetMiscellaneousDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int miscellaneousId, int applicantId)
        {
            var viewModel = new MiscellaneousItemsViewModel()
            {
                MiscellaneousItemDtos = _item.GetItemDtosByMiscellaneous(miscellaneousId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

    }
}