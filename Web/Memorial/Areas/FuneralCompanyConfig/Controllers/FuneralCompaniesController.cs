using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.FuneralCompany;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.FuneralCompanyConfig.Controllers
{
    public class FuneralCompaniesController : Controller
    {
        private readonly IFuneralCompany _funeralCompany;
        private readonly ICatalog _catalog;

        public FuneralCompaniesController(IFuneralCompany funeralCompany, ICatalog catalog)
        {
            _funeralCompany = funeralCompany;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new FuneralCompanyFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesUrn());

            var dto = new FuneralCompanyDto();
            if (id != null)
            {
                dto = Mapper.Map<FuneralCompanyDto>(_funeralCompany.Get((int)id));
            }

            vw.FuneralCompanyDto = dto;

            return View(vw);
        }

    }
}