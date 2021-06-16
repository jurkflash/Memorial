using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.CemeteryLandscapeCompany;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;

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

            vw.SiteDtos = _catalog.GetSiteDtosUrn();

            var dto = new CemeteryLandscapeCompanyDto();
            if (id != null)
            {
                dto = _cemeteryLandscapeCompany.GetCemeteryLandscapeCompanyDto((int)id);
            }

            vw.CemeteryLandscapeCompanyDto = dto;

            return View(vw);
        }

    }
}