using System.Web.Mvc;

namespace Memorial.Areas.AncestralTabletConfig.Controllers
{
    public class ItemsController : Controller
    {
        // GET: AncestralTabletConfig/Areas
        public ActionResult Index(int areaId)
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }
    }
}