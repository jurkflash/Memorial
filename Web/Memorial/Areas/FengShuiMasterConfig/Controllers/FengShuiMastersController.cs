using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.FengShuiMaster;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.FengShuiMasterConfig.Controllers
{
    public class FengShuiMastersController : Controller
    {
        private readonly IFengShuiMaster _fengShuiMaster;
        private readonly ICatalog _catalog;

        public FengShuiMastersController(FengShuiMaster fengShuiMaster, ICatalog catalog)
        {
            _fengShuiMaster = fengShuiMaster;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new FengShuiMasterFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesUrn());

            var dto = new FengShuiMasterDto();
            if (id != null)
            {
                dto = Mapper.Map<FengShuiMasterDto>(_fengShuiMaster.Get((int)id));
            }

            vw.FengShuiMasterDto = dto;

            return View(vw);
        }

    }
}