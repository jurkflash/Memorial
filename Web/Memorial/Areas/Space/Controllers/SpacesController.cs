using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Lib.Site;
using Memorial.Lib.Space;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Areas.Space.Controllers
{
    public class SpacesController : Controller
    {
        private readonly ISpace _space;
        private readonly ISite _site;
        private readonly IItem _item;
        private readonly ITransaction _transaction;

        public SpacesController(
            ISpace space,
            ISite site,
            IItem item,
            ITransaction transaction
            )
        {
            _space = space;
            _site = site;
            _item = item;
            _transaction = transaction;
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
            List<RecentDto> recents = new List<RecentDto>();

            var transactions = _transaction.GetRecent(null, siteId);

            foreach(var transaction in transactions)
            {
                recents.Add(new RecentDto()
                {
                    Code = transaction.AF,
                    ApplicantName = transaction.ApplicantDto.Name,
                    CreateDate = transaction.CreatedDate,
                    ItemId = transaction.SpaceItemDto.Id,
                    Text1 = transaction.SpaceItemDto.SpaceDto.Name,
                    ItemName = transaction.SpaceItemDto.SubProductServiceDto.Name,
                    LinkArea = transaction.SpaceItemDto.SubProductServiceDto.ProductDto.Area,
                    LinkController = transaction.SpaceItemDto.SubProductServiceDto.SystemCode
                });
            }

            return View(recents);
        }

    }
}