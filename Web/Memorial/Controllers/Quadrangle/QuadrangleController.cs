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
    public class QuadrangleController : Controller
    {
        private readonly IQuadrangle _quadrangle;
        private readonly IQuadrangleArea _quadrangleArea;
        private readonly IQuadrangleCentre _quadrangleCentre;
        private readonly IQuadrangleItem _quadrangleItem;

        public QuadrangleController(IQuadrangle quadrangle, IQuadrangleCentre quadrangleCentre, IQuadrangleArea quadrangleArea, IQuadrangleItem quadrangleItem)
        {
            _quadrangle = quadrangle;
            _quadrangleArea = quadrangleArea;
            _quadrangleCentre = quadrangleCentre;
            _quadrangleItem = quadrangleItem;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            return RedirectToAction("Centre", new { siteId = siteId, applicantId = applicantId });
        }

        public ActionResult Centre(byte siteId, int applicantId)
        {
            var viewModel = new QuadrangleCentreIndexesViewModel()
            {
                QuadrangleCentreDtos = _quadrangleCentre.DtosGetBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Area(int centreId, int applicantId)
        {
            var viewModel = new QuadrangleAreaIndexesViewModel()
            {
                QuadrangleAreaDtos = _quadrangleArea.DtosGetByCentre(centreId),
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

        public ActionResult Items(int id, int areaId, int centreId, int applicantId)
        {
            var quad = _quadrangle.DtosGetById(id);
            var viewModel = new QuadrangleItemsViewModel()
            {
                QuadrangleItemDtos = _quadrangleItem.DtosGetByCentre(centreId),
                QuadrangleId = quad.Id,
                QuadrangleAreaId = areaId,
                QuadrangleCentreId = centreId,
                QuadrangleName = quad.Name,
                QuadrangleDescription = quad.Description,
                applicantId = applicantId
            };
            return View(viewModel);
        }
    }
}