using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Areas.ColumbariumConfig.Controllers
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

        public ActionResult Form(int? id)
        {
            var dto = new ColumbariumItemDto();
            if (id != null)
            {
                dto = Mapper.Map<ColumbariumItemDto>(_item.GetById((int)id));
            }
            return View(dto);
        }
    }
}