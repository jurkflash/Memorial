﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;

namespace Memorial.Controllers.Plot
{
    public class PlotDoubleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlotDoubleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int plotItemId)
        {
            var plotItem = _unitOfWork.PlotItems.Get(plotItemId);
            var plots = _unitOfWork.Plots.GetByTypeAndArea(plotItem.Id, plotItem.PlotAreaId);
            return View(plots);
        }

        public ActionResult Order(int plotId)
        {

            return View();
        }
    }
}