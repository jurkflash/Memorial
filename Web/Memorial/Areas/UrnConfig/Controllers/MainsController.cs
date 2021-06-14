using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Urn;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;

namespace Memorial.Areas.UrnConfig.Controllers
{
    public class MainsController : Controller
    {
        private readonly IUrn _urn;
        private readonly ICatalog _catalog;

        public MainsController(IUrn urn, ICatalog catalog)
        {
            _urn = urn;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new UrnFormViewModel();

            vw.SiteDtos = _catalog.GetSiteDtosUrn();

            var dto = new UrnDto();
            if (id != null)
            {
                dto = _urn.GetUrnDto((int)id);
            }

            vw.UrnDto = dto;

            return View(vw);
        }

    }
}