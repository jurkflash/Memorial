using System.Web.Mvc;
using Memorial.Lib.Cremation;
using Memorial.Core.Dtos;
using AutoMapper;

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
                dto = Mapper.Map<CremationItemDto>(_item.GetById((int)id));
            }
            return View(dto);
        }
    }
}