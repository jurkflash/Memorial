using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;
using Memorial.Lib;
using Memorial.Lib.Quadrangle;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Controllers
{
    public class QuadrangleController : Controller
    {
        private readonly IQuadrangle _quadrangle;
        private readonly IArea _area;
        private readonly ICentre _centre;
        private readonly IItem _item;

        public QuadrangleController(IQuadrangle quadrangle, ICentre centre, IArea area, IItem item)
        {
            _quadrangle = quadrangle;
            _area = area;
            _centre = centre;
            _item = item;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            return RedirectToAction("Centre", new { siteId = siteId, applicantId = applicantId });
        }

        public ActionResult Centre(byte siteId, int applicantId)
        {
            var viewModel = new QuadrangleCentreIndexesViewModel()
            {
                QuadrangleCentreDtos = _centre.DtosGetBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Area(int centreId, int applicantId)
        {
            var viewModel = new QuadrangleAreaIndexesViewModel()
            {
                QuadrangleAreaDtos = _area.DtosGetByCentre(centreId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Quadrangle(int areaId, int applicantId)
        {
            var viewModel = new QuadrangleIndexesViewModel()
            {
                QuadrangleDtos = _quadrangle.DtosGetByArea(areaId),
                Positions = _quadrangle.GetPositionsByArea(areaId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int id, int applicantId)
        {
            _quadrangle.SetQuadrangle(id);
            var viewModel = new QuadrangleItemsViewModel()
            {
                QuadrangleItemDtos = Mapper.Map<IEnumerable<Core.Domain.QuadrangleItem>,IEnumerable<QuadrangleItemDto>>(_quadrangle.GetItems()),
                QuadrangleId = id,
                QuadrangleName = _quadrangle.GetName(),
                QuadrangleDescription = _quadrangle.GetName(),
                applicantId = applicantId
            };
            return View(viewModel);
        }
    }
}