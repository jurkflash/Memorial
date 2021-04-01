using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;
using Memorial.Lib.Plot;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Areas.Plot.Controllers
{
    public class PlotController : Controller
    {
        private readonly IPlot _plot;
        private readonly IArea _area;
        private readonly IItem _item;

        public PlotController(
            IPlot plot,
            IArea area,
            IItem item)
        {
            _plot = plot;
            _area = area;
            _item = item;
        }


        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new PlotAreaIndexesViewModel()
            {
                PlotAreaDtos = _area.GetAreaDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Plot(int areaId, int applicantId)
        {
            var viewModel = new PlotIndexesViewModel()
            {
                PlotDtos = _plot.GetPlotDtosByAreaId(areaId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int id, int applicantId)
        {
            _plot.SetPlot(id);
            var viewModel = new PlotItemsViewModel()
            {
                PlotItemDtos = _item.GetItemDtosByPlot(id),
                PlotDto = _plot.GetPlotDto(),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

    }
}