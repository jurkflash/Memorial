using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.ViewModels;

namespace Memorial.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var sites = _unitOfWork.Sites.GetAll();

            return View(sites);
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

        public ActionResult Item(string catalog, byte siteId, int applicantId)
        {
            return RedirectToAction("Index", catalog, new { siteId = siteId, applicantId = applicantId });
        }
    }
}