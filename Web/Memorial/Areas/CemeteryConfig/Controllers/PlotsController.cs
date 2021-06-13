using System.Web.Mvc;
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
                dto = _plot.GetPlotDto((int)id);
            }
            return View(dto);
        }
    }
}