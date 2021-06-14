using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;

namespace Memorial.Areas.ColumbariumConfig.Controllers
{
    public class NichesController : Controller
    {
        private readonly INiche _niche;
        public NichesController(INiche niche)
        {
            _niche = niche;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var dto = new NicheDto();
            if (id != null)
            {
                dto = _niche.GetNicheDto((int)id);
            }
            return View(dto);
        }
    }
}