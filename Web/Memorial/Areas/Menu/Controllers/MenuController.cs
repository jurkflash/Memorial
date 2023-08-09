using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Lib.Site;
using Memorial.Lib.Catalog;
using AutoMapper;
using Memorial.Core.Dtos;

namespace Memorial.Areas.Menu.Controllers
{
    public class MenuController : Controller
    {
        private readonly ISite _site;
        private readonly ICatalog _catalog;

        public MenuController(ISite site, ICatalog catalog)
        {
            _site = site;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<SiteDto>>(_site.GetAll()));
        }

        public ActionResult Catalog(byte siteId, int? applicantId)
        {
            var viewModel = new ListCatalogViewModel()
            {
                CatalogDtos = Mapper.Map<IEnumerable<CatalogDto>>(_catalog.GetBySite(siteId)),
                SiteId = siteId,
                SiteDto = Mapper.Map<SiteDto>(_site.Get(siteId)),
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