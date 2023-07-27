using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Persistence;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Memorial.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        
    }
}