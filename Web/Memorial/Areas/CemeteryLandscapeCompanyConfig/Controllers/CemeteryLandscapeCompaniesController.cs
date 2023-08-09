using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.CemeteryLandscapeCompany;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.CemeteryLandscapeCompanyConfig.Controllers
{
    public class CemeteryLandscapeCompaniesController : Controller
    {
        private readonly ICemeteryLandscapeCompany _cemeteryLandscapeCompany;
        private readonly ICatalog _catalog;

        public CemeteryLandscapeCompaniesController(CemeteryLandscapeCompany cemeteryLandscapeCompany, ICatalog catalog)
        {
            _cemeteryLandscapeCompany = cemeteryLandscapeCompany;
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var vw = new CemeteryLandscapeCompanyFormViewModel();

            vw.SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesUrn());

            var dto = new CemeteryLandscapeCompanyDto();
            if (id != null)
            {
                dto = Mapper.Map<CemeteryLandscapeCompanyDto>(_cemeteryLandscapeCompany.Get((int)id));
            }

            vw.CemeteryLandscapeCompanyDto = dto;

            return View(vw);
        }

    }
}