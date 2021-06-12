using System.Web.Mvc;
using Memorial.Lib.AncestralTablet;
using Memorial.Lib.Catalog;
using Memorial.Core.Dtos;

namespace Memorial.Areas.AncestralTabletConfig.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItem _item;
        private readonly ICatalog _catalog;

        public ItemsController(IItem item, ICatalog catalog)
        {
            _item = item;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var dto = new AncestralTabletItemDto();
            if (id != null)
            {
                dto = _item.GetItemDto((int)id);
            }
            return View(dto);
        }
    }
}