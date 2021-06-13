using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Cemetery;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;

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

            vw.SiteDtos = _catalog.GetSiteDtosAncestralTablet();

            var dto = new CemeteryAreaDto();
            if (id != null)
            {
                dto = _area.GetAreaDto((int)id);
            }

            vw.CemeteryAreaDto = dto;

            return View(vw);
        }

    }
}