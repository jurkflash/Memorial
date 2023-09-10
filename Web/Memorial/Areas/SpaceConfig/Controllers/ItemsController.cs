using System.Web.Mvc;
using Memorial.Lib.Space;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Areas.SpaceConfig.Controllers
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
            var dto = new SpaceItemDto();
            if (id != null)
            {
                dto = Mapper.Map<SpaceItemDto>(_item.GetById((int)id));
            }
            return View(dto);
        }
    }
}