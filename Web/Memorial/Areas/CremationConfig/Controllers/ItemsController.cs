using System.Web.Mvc;
using Memorial.Lib.Cremation;
using Memorial.Lib.Catalog;
using Memorial.Core.Dtos;

namespace Memorial.Areas.CremationConfig.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItem _item;

        public ItemsController(IItem item)
        {
            _item = item;
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
            var dto = new CremationItemDto();
            if (id != null)
            {
                dto = _item.GetItemDto((int)id);
            }
            return View(dto);
        }
    }
}