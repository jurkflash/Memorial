using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.ColumbariumConfig.Controllers
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
            var vw = new ColumbariumAreaFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesColumbarium());

            var dto = new ColumbariumAreaDto();
            if (id != null)
            {
                dto = Mapper.Map<ColumbariumAreaDto>(_area.GetById((int)id));
            }

            vw.ColumbariumAreaDto = dto;

            return View(vw);
        }

    }
}