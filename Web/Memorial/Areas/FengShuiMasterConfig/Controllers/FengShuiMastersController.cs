using System.Web.Mvc;
using Memorial.Core.Dtos;
using Memorial.Lib.FengShuiMaster;
using Memorial.Lib.Catalog;
using Memorial.ViewModels;

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

            vw.SiteDtos = _catalog.GetSiteDtosUrn();

            var dto = new FengShuiMasterDto();
            if (id != null)
            {
                dto = _fengShuiMaster.GetFengShuiMasterDto((int)id);
            }

            vw.FengShuiMasterDto = dto;

            return View(vw);
        }

    }
}