using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

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

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesColumbarium());

            var dto = new ColumbariumCentreDto();
            if (id != null)
            {
                dto = Mapper.Map<ColumbariumCentreDto>(_centre.GetById((int)id));
            }

            vw.ColumbariumCentreDto = dto;

            return View(vw);
        }

    }
}