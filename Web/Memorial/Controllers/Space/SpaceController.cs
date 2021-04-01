using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Lib.Space;

namespace Memorial.Controllers
{
    public class SpaceController : Controller
    {
        private readonly ISpace _space;
        private readonly IItem _item;

        public SpaceController(
            ISpace space,
            IItem item)
        {
            _space = space;
            _item = item;
        }

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new SpaceIndexesViewModel()
            {
                SpaceDtos = _space.DtosGetBySite(siteId),
                ApplicantId = applicantId
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

    }
}