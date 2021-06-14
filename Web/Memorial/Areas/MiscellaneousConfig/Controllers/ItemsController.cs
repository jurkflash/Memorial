using System.Web.Mvc;
using Memorial.Lib.Miscellaneous;
using Memorial.Core.Dtos;

namespace Memorial.Areas.MiscellaneousConfig.Controllers
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
            var dto = new MiscellaneousItemDto();
            if (id != null)
            {
                dto = _item.GetItemDto((int)id);
            }
            return View(dto);
        }
    }
}