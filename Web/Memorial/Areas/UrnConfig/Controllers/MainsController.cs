using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Urn;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.UrnConfig.Controllers
{
    public class MainsController : Controller
    {
        private readonly IUrn _urn;
        private readonly ICatalog _catalog;

        public MainsController(IUrn urn, ICatalog catalog)
        {
            _urn = urn;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new UrnFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesUrn());

            var dto = new UrnDto();
            if (id != null)
            {
                dto = Mapper.Map<UrnDto>(_urn.Get((int)id));
            }

            vw.UrnDto = dto;

            return View(vw);
        }

    }
}