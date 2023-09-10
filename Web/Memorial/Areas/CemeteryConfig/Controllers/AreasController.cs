using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Cemetery;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.CemeteryConfig.Controllers
{
    public class AreasController : Controller
    {
        private readonly IArea _area;
        private readonly ICatalog _catalog;

        public AreasController(IArea area, ICatalog catalog)
        {
            _area = area;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new CemeteryAreaFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesAncestralTablet());

            var dto = new CemeteryAreaDto();
            if (id != null)
            {
                dto = Mapper.Map<CemeteryAreaDto>(_area.GetById((int)id));
            }

            vw.CemeteryAreaDto = dto;

            return View(vw);
        }

    }
}