using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;

namespace Memorial.Controllers
{
    public class PlotController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlotController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var plotAreas = _unitOfWork.PlotAreas.GetBySite(siteId);
            return View(plotAreas);
        }

        public ActionResult Items(int plotAreaId)
        {
            var plotItems = _unitOfWork.PlotItems.GetByArea(plotAreaId);
            return View(plotItems);
        }

        public ActionResult Item(string item, int plotItemId)
        {
            return RedirectToAction("Index", "Plot" + item, new { plotItemId = plotItemId });
        }    

    }
}