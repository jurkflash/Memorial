using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.ViewModels;

namespace Memorial.Controllers.Ancestor
{
    public class AncestorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AncestorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(byte siteId, int applicantId)
        {
            var ancestorAreas = _unitOfWork.AncestorAreas.GetBySite(siteId);
            return View(ancestorAreas);
        }

        public ActionResult Ancestor(int ancestorAreaId)
        {
            var ancestors = _unitOfWork.Ancestors.GetByArea(ancestorAreaId);
            return View(ancestors);
        }

        public ActionResult Items(int ancestorId, int ancestorAreaId)
        {
            var listAncestorItemsViewModel = new ListAncestorItemsViewModel()
            {
                AncestorItems = _unitOfWork.AncestorItems.GetByArea(ancestorAreaId),
                Ancestor = _unitOfWork.Ancestors.Get(ancestorId),
                AncestorArea = _unitOfWork.AncestorAreas.Get(ancestorAreaId)
            };

            return View(listAncestorItemsViewModel);
        }

        public ActionResult Item(string item, int ancestorItemId, int ancestorId, int ancestorAreaId)
        {
            return RedirectToAction("Index", "Ancestor" + item, new { ancestorItemId = ancestorItemId, ancestorId = ancestorId, ancestorAreaId = ancestorAreaId });
        }
    }
}