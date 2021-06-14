using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;

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

            vw.SiteDtos = _catalog.GetSiteDtosColumbarium();

            var dto = new ColumbariumAreaDto();
            if (id != null)
            {
                dto = _area.GetAreaDto((int)id);
            }

            vw.ColumbariumAreaDto = dto;

            return View(vw);
        }

    }
}