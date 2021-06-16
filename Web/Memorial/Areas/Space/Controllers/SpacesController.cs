using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Lib.Site;
using Memorial.Lib.Space;

namespace Memorial.Areas.Space.Controllers
{
    public class SpacesController : Controller
    {
        private readonly ISpace _space;
        private readonly ISite _site;
        private readonly IItem _item;

        public SpacesController(
            ISpace space,
            ISite site,
            IItem item)
        {
            _space = space;
            _site = site;
            _item = item;
        }

        public ActionResult Index(int siteId, int applicantId = 0)
        {
            var viewModel = new SpaceIndexesViewModel()
            {
                SpaceDtos = _space.GetSpaceDtosBySite(siteId),
                ApplicantId = applicantId,
                siteDto = _site.GetSiteDto(siteId)
            };
            return View(viewModel);
        }

        public ActionResult ShowCalendar(int siteId)
        {
            var viewModel = new SpaceIndexesViewModel()
            {
                SpaceDtos = _space.GetSpaceDtosBySite(siteId),
                siteDto = _site.GetSiteDto(siteId)
            };
            return View(viewModel);
        }

        public ActionResult Items(int spaceId, int applicantId)
        {
            var viewModel = new SpaceItemsViewModel()
            {
                SpaceItemDtos = _item.GetItemDtosBySpace(spaceId),
                ApplicantId = applicantId
            };

            return View(viewModel);
        }

        public ActionResult Menu(int siteId)
        {

            return View();
        }

    }
}