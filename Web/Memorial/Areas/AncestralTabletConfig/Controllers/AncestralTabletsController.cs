using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Areas.AncestralTabletConfig.Controllers
{
    public class AncestralTabletsController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        public AncestralTabletsController(IAncestralTablet ancestralTablet)
        {
            _ancestralTablet = ancestralTablet;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var dto = new AncestralTabletDto();
            if (id != null)
            {
                dto = _ancestralTablet.GetAncestralTabletDto((int)id);
            }
            return View(dto);
        }
    }
}