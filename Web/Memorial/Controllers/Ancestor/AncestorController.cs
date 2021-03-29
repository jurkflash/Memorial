using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Lib;
using Memorial.Lib.Applicant;
using Memorial.Lib.Ancestor;

namespace Memorial.Controllers.Ancestor
{
    public class AncestorController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IAncestor _ancestor;
        private readonly IArea _area;
        private readonly IItem _item;

        public AncestorController(
            IApplicant applicant,
            IAncestor ancestor,
            IArea area,
            IItem item)
        {
            _applicant = applicant;
            _ancestor = ancestor;
            _area = area;
            _item = item;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var viewModel = new AncestorAreaIndexesViewModel()
            {
                AncestorAreaDtos = _area.GetAreaDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Ancestor(int areaId, int applicantId)
        {
            var viewModel = new AncestorIndexesViewModel()
            {
                AncestorDtos = _ancestor.GetAncestorDtosByAreaId(areaId),
                Positions = _ancestor.GetPositionsByAreaId(areaId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int id, int applicantId)
        {
            _ancestor.SetAncestor(id);
            _area.SetArea(_ancestor.GetAreaId());
            var viewModel = new AncestorItemsViewModel()
            {
                AncestorItemDtos = _item.GetItemDtosByArea(_area.GetId()),
                AncestorDto = _ancestor.GetAncestorDto(),
                ApplicantDto = _applicant.GetApplicantDto(applicantId)
            };
            return View(viewModel);
        }

    }
}