using System.Web.Mvc;
using AutoMapper;
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
                dto = Mapper.Map< AncestralTabletDto>(_ancestralTablet.GetById((int)id));
            }
            return View(dto);
        }
    }
}