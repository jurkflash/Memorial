using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Lib.Site;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.Ancestor;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class AncestorsController : Controller
    {
        private readonly IAncestor _ancestor;
        private readonly ISite _site;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly IApplicantDeceased _applicantDeceased;

        public AncestorsController(
            IAncestor ancestor,
            ISite site,
            IArea area,
            IItem item,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased)
        {
            _ancestor = ancestor;
            _site = site;
            _area = area;
            _item = item;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
        }

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new AncestorAreaIndexesViewModel()
            {
                AncestorAreaDtos = _area.GetAreaDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Ancestors(int areaId, int applicantId)
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
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult AncestorInfo(int id)
        {
            var viewModel = new AncestorInfoViewModel();
            _ancestor.SetAncestor(id);

            if (_ancestor.GetAncestorDto() != null)
            {
                viewModel.AncestorDto = _ancestor.GetAncestorDto();
                viewModel.AncestorAreaDto = _area.GetAreaDto(_ancestor.GetAreaId());
                viewModel.SiteDto = _site.GetSiteDto(viewModel.AncestorAreaDto.SiteId);

                if (_ancestor.HasApplicant())
                {
                    viewModel.ApplicantDto = _applicant.GetApplicantDto((int)_ancestor.GetApplicantId());
                    var deceaseds = _deceased.GetDeceasedsByAncestorId(_ancestor.GetAncestor().Id).ToList();
                    if (deceaseds.Count > 0)
                    {
                        viewModel.DeceasedFlattenDto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_ancestor.GetApplicantId(), deceaseds[0].Id);
                    }
                }
            }

            return PartialView("_AncestorInfo", viewModel);
        }

    }
}