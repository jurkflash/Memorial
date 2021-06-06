using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memorial.Areas.SiteConfig.Controllers
{
    public class SitesController : Controller
    {
        // GET: SiteConfig/Sites
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }
    }
}