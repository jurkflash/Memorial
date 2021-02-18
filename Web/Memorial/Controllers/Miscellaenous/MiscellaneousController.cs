using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;
using Memorial.Lib;

namespace Memorial.Controllers.Miscellaenous
{
    public class MiscellaneousController : Controller
    {
        private readonly IMiscellaneous _miscellaneous;
        private readonly IMiscellaneousItem _miscellaneousItem;

        public MiscellaneousController(IMiscellaneous miscellaneous, IMiscellaneousItem miscellaneousItem)
        {
            _miscellaneous = miscellaneous;
            _miscellaneousItem = miscellaneousItem;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var viewModel = new MiscellaneousIndexesViewModel()
            {
                MiscellaneousDtos = _miscellaneous.DtosGetBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int miscellaneousId, int applicantId)
        {
            var viewModel = new MiscellaneousItemsViewModel()
            {
                MiscellaneousItemDtos = _miscellaneousItem.DtosGetByMiscellaneous(miscellaneousId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Item(int miscellaneousItemId, int applicantId)
        {
            return RedirectToAction("Index", "MiscellaneousTransactions", new { miscellaneousItemId = miscellaneousItemId, applicantId = applicantId });
        }

    }
}