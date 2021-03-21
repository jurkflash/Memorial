using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;
using Memorial.Lib;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.Quadrangle;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Controllers
{
    public class QuadrangleController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly IApplicantDeceased _applicantDeceased;
        private readonly IQuadrangle _quadrangle;
        private readonly IArea _area;
        private readonly ICentre _centre;
        private readonly IItem _item;
        private readonly ISite _site;

        public QuadrangleController(
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased, 
            IQuadrangle quadrangle, 
            ICentre centre, 
            IArea area, 
            IItem item,
            ISite site)
        {
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _quadrangle = quadrangle;
            _area = area;
            _centre = centre;
            _item = item;
            _site = site;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            return Centre(siteId, applicantId);
        }

        public ActionResult Centre(byte siteId, int applicantId)
        {
            var viewModel = new QuadrangleCentreIndexesViewModel()
            {
                QuadrangleCentreDtos = _centre.GetCentreDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View("Centre", viewModel);
        }

        public ActionResult Area(int centreId, int applicantId)
        {
            var viewModel = new QuadrangleAreaIndexesViewModel()
            {
                QuadrangleAreaDtos = _area.GetAreaDtosByCentre(centreId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Quadrangle(int areaId, int applicantId)
        {
            var viewModel = new QuadrangleIndexesViewModel()
            {
                QuadrangleDtos = _quadrangle.GetQuadrangleDtosByAreaId(areaId),
                Positions = _quadrangle.GetPositionsByAreaId(areaId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int id, int applicantId)
        {
            _quadrangle.SetQuadrangle(id);
            _area.SetArea(_quadrangle.GetAreaId());
            _centre.SetCentre(_area.GetCentreId());
            var viewModel = new QuadrangleItemsViewModel()
            {
                QuadrangleItemDtos = _item.GetItemDtosByCentre(_centre.GetID()),
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                ApplicantDto = _applicant.GetApplicantDto(applicantId)
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult QuadrangleInfo(int id)
        {
            var viewModel = new QuadrangleInfoViewModel();
            _quadrangle.SetQuadrangle(id);
            
            if(_quadrangle.GetQuadrangleDto() != null)
            {
                viewModel.QuadrangleDto = _quadrangle.GetQuadrangleDto();
                viewModel.QuadrangleAreaDto = _area.GetAreaDto(_quadrangle.GetQuadrangleDto().QuadrangleAreaDtoId);
                viewModel.QuadrangleCentreDto = _centre.GetCentreDto(viewModel.QuadrangleAreaDto.QuadrangleCentreDtoId);
                viewModel.SiteDto = _site.GetSiteDto(viewModel.QuadrangleCentreDto.SiteId);

                if(_quadrangle.HasApplicant())
                {
                    viewModel.ApplicantDto = _applicant.GetApplicantDto((int)_quadrangle.GetApplicantId());
                    var deceaseds = _deceased.GetDeceasedsByQuadrangleId(_quadrangle.GetQuadrangle().Id).ToList();
                    if(deceaseds.Count > 0)
                    {
                        viewModel.DeceasedFlatten1Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_quadrangle.GetApplicantId(), deceaseds[0].Id);
                    }
                    if (deceaseds.Count > 1)
                    {
                        viewModel.DeceasedFlatten2Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_quadrangle.GetApplicantId(), deceaseds[1].Id);
                    }
                }
            }

            return PartialView("_QuadrangleInfo", viewModel);
        }
    }
}