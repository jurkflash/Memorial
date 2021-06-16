using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.FuneralCompany;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;

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

            vw.SiteDtos = _catalog.GetSiteDtosUrn();

            var dto = new FuneralCompanyDto();
            if (id != null)
            {
                dto = _funeralCompany.GetFuneralCompanyDto((int)id);
            }

            vw.FuneralCompanyDto = dto;

            return View(vw);
        }

    }
}