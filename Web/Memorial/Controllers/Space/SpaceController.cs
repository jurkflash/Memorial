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
    public class SpaceController : Controller
    {
        private readonly ISpace _space;
        private readonly ISpaceItem _spaceItem;

        public SpaceController(ISpace space, ISpaceItem spaceItem)
        {
            _space = space;
            _spaceItem = spaceItem;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var viewModel = new SpaceIndexesViewModel()
            {
                SpaceDtos = _space.DtosGetBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int spaceId, int applicantId)
        {
            var viewModel = new SpaceItemsViewModel()
            {
                SpaceItemDtos = _spaceItem.DtosGetBySpace(spaceId),
                ApplicantId = applicantId
            };

            return View(viewModel);
        }

        public ActionResult Item(int spaceItemId, int applicantId)
        {
            return RedirectToAction("Index", "SpaceTransactions", new { spaceItemId = spaceItemId, applicantId = applicantId });
        }

    }
}