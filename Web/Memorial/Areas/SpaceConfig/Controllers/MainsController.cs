using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;

namespace Memorial.Areas.SpaceConfig.Controllers
{
    public class MainsController : Controller
    {
        private readonly ISpace _space;
        private readonly ICatalog _catalog;

        public MainsController(ISpace space, ICatalog catalog)
        {
            _space = space;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new SpaceFormViewModel();

            vw.SiteDtos = _catalog.GetSiteDtosSpace();

            var dto = new SpaceDto();
            if (id != null)
            {
                dto = _space.GetSpaceDto((int)id);
            }

            vw.SpaceDto = dto;

            return View(vw);
        }

    }
}