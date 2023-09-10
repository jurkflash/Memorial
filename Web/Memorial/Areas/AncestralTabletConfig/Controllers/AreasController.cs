using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;

namespace Memorial.Areas.AncestralTabletConfig.Controllers
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
            var vw = new AncestralTabletAreaFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesAncestralTablet());

            var dto = new AncestralTabletAreaDto();
            if (id != null)
            {
                dto = Mapper.Map<AncestralTabletAreaDto>(_area.GetById((int)id));
            }

            vw.AncestralTabletAreaDto = dto;

            return View(vw);
        }

    }
}