﻿using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;

namespace Memorial.Areas.ColumbariumConfig.Controllers
{
    public class CentresController : Controller
    {
        private readonly ICentre _centre;
        private readonly ICatalog _catalog;

        public CentresController(ICentre centre, ICatalog catalog)
        {
            _centre = centre;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new ColumbariumCentreFormViewModel();

            vw.SiteDtos = _catalog.GetSiteDtosColumbarium();

            var dto = new ColumbariumCentreDto();
            if (id != null)
            {
                dto = _centre.GetCentreDto((int)id);
            }

            vw.ColumbariumCentreDto = dto;

            return View(vw);
        }

    }
}