using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using Memorial.Lib.Site;
using Memorial.Lib.Catalog;

namespace Memorial.Areas.Menu.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISite _site;
        private readonly ICatalog _catalog;

        public MenuController(IUnitOfWork unitOfWork, ISite site, ICatalog catalog)
        {
            _unitOfWork = unitOfWork;
            _site = site;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View(_site.GetSites());
        }

        public ActionResult Catalog(byte siteId, int? applicantId)
        {
            var viewModel = new ListCatalogViewModel()
            {
                CatalogDtos = _catalog.GetCatalogDtosBySite(siteId),
                SiteId = siteId,
                SiteDto = _site.GetSiteDto(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Recent(byte siteId, int? applicantId)
        {
            var viewModel = new RecentViewModel()
            {
                SiteId = siteId,
                ApplicantId = applicantId
            };
            return View(viewModel);
        }
    }
}