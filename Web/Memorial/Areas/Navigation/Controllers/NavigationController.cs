using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Areas.Navigation.Controllers
{
    public class NavigationController : Controller
    {
        private readonly ISite _site;

        public NavigationController(ISite site)
        {
            _site = site;
        }

        [ChildActionOnly]
        public ActionResult Navigation(int? applicantId)
        {
            var s = _site.GetAll();
            var viewModel = new NavigationViewModel()
            {
                SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_site.GetAll()),
                ApplicantId = applicantId
            };
            
            return PartialView("_Navigation", viewModel);
        }

        public EmptyResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return new EmptyResult();
        }
    }
}