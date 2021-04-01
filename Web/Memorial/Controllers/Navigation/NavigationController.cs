using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;

namespace Memorial.Controllers.Navigation
{
    [ChildActionOnly]
    public class NavigationController : Controller
    {
        private readonly ISite _site;

        public NavigationController(ISite site)
        {
            _site = site;
        }

        public ActionResult Navigation(int? applicantId = 0)
        {
            var viewModel = new NavigationViewModel()
            {
                SiteDtos = _site.GetSiteDtos(),
                ApplicantId = (int)applicantId
            };
            
            return PartialView("_Navigation", viewModel);
        }
    }
}