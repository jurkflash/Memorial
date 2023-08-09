using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.SpaceConfig.Controllers
{
    public class MainsController : Controller
    {
        private readonly ISpace _space;
        private readonly ICatalog _catalog;

        public MainsController(ISpace space, ICatalog catalog)
        {
            _space = space;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new SpaceFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesSpace());

            var dto = new SpaceDto();
            if (id != null)
            {
                dto = Mapper.Map<SpaceDto>(_space.Get((int)id));
            }

            vw.SpaceDto = dto;

            return View(vw);
        }

    }
}