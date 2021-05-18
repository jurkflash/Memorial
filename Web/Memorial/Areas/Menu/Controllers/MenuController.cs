using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using Memorial.Lib.Site;

namespace Memorial.Areas.Menu.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ISite _site;

        public MenuController(IUnitOfWork unitOfWork, ISite site)
        {
            _unitOfWork = unitOfWork;
            _site = site;
        }

        public ActionResult Index()
        {
            return View(_site.GetSites());
        }

        public ActionResult Catalog(byte siteId, int applicantId)
        {
            var viewModel = new ListCatalogViewModel()
            {
                Catalogs = _unitOfWork.Catalogs.GetBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }
    }
}