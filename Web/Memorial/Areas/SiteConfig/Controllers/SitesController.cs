﻿using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Dtos;
using Memorial.Lib.Site;
using System.Web.Mvc;
using AutoMapper;

namespace Memorial.Areas.SiteConfig.Controllers
{
    public class SitesController : Controller
    {
        private readonly ISite _site;

        public SitesController(ISite site)
        {
            _site = site;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var siteDto = new SiteDto();
            if(id != null)
            {
                siteDto = Mapper.Map<SiteDto>(_site.Get((int)id));
            }
            return View(siteDto);
        }
    }
}