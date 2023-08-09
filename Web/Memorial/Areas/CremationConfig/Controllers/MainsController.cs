using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Cremation;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.CremationConfig.Controllers
{
    public class MainsController : Controller
    {
        private readonly ICremation _cremation;
        private readonly ICatalog _catalog;

        public MainsController(ICremation cremation, ICatalog catalog)
        {
            _cremation = cremation;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new CremationFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesCremation());

            var dto = new CremationDto();
            if (id != null)
            {
                dto = _cremation.GetCremationDto((int)id);
            }

            vw.CremationDto = dto;

            return View(vw);
        }

    }
}