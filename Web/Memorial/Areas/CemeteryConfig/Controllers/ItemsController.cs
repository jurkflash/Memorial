using System.Web.Mvc;
using Memorial.Lib.Cemetery;
using Memorial.Lib.Catalog;
using Memorial.Core.Dtos;

namespace Memorial.Areas.CemeteryConfig.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItem _item;

        public ItemsController(IItem item)
        {
            _item = item;
        }

        public ActionResult Form(int? id)
        {
            var dto = new CemeteryItemDto();
            if (id != null)
            {
                dto = _item.GetItemDto((int)id);
            }
            return View(dto);
        }
    }
}