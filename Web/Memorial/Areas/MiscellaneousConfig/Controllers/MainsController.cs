using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.Miscellaneous;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.MiscellaneousConfig.Controllers
{
    public class MainsController : Controller
    {
        private readonly IMiscellaneous _miscellaneous;
        private readonly ICatalog _catalog;

        public MainsController(IMiscellaneous miscellaneous, ICatalog catalog)
        {
            _miscellaneous = miscellaneous;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new MiscellaneousFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesMiscellaneous());

            var dto = new MiscellaneousDto();
            if (id != null)
            {
                dto = Mapper.Map<MiscellaneousDto>(_miscellaneous.Get((int)id));
            }

            vw.MiscellaneousDto = dto;

            return View(vw);
        }

    }
}