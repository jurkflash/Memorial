using System.Web.Mvc;
using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Lib.Cemetery;

namespace Memorial.Areas.CemeteryConfig.Controllers
{
    public class PlotsController : Controller
    {
        private readonly IPlot _plot;
        public PlotsController(IPlot plot)
        {
            _plot = plot;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var dto = new PlotDto();
            if (id != null)
            {
                dto = Mapper.Map<PlotDto>(_plot.GetById((int)id));
            }
            return View(dto);
        }
    }
}