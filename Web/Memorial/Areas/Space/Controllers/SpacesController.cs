using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Lib.Site;
using Memorial.Lib.Space;
using Memorial.Core.Dtos;
using System.Collections.Generic;
using AutoMapper;

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

        public ActionResult Index(int siteId, int? applicantId)
        {
            var viewModel = new SpaceIndexesViewModel();
            viewModel.SpaceDtos = Mapper.Map<IEnumerable<SpaceDto>>(_space.GetBySite(siteId));
            viewModel.SiteDto = Mapper.Map<SiteDto>(_site.Get(siteId));

            if (applicantId != null)
            {
                viewModel.ApplicantId = applicantId;
            }

            return View(viewModel);
        }

        public ActionResult ShowCalendar(int siteId)
        {
            var viewModel = new SpaceIndexesViewModel()
            {
                SpaceDtos = Mapper.Map<IEnumerable<SpaceDto>>(_space.GetBySite(siteId)),
                SiteDto = Mapper.Map<SiteDto>(_site.Get(siteId))
            };
            return View(viewModel);
        }

        public ActionResult Items(int spaceId, int applicantId)
        {
            var viewModel = new SpaceItemsViewModel()
            {
                SpaceDto = Mapper.Map<SpaceDto>(_space.Get(spaceId)),
                SpaceItemDtos = _item.GetItemDtosBySpace(spaceId),
                ApplicantId = applicantId
            };

            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult Recent(int siteId, int? applicantId)
        {
            List<RecentDto> recents = new List<RecentDto>();

            var transactions = Mapper.Map<IEnumerable<SpaceTransactionDto>>(_transaction.GetRecent(siteId, applicantId));

            foreach (var transaction in transactions)
            {
                recents.Add(new RecentDto()
                {
                    Code = transaction.AF,
                    ApplicantName = transaction.ApplicantDto.Name,
                    CreatedDate = transaction.CreatedUtcTime,
                    ItemId = transaction.SpaceItemDto.Id,
                    Text1 = transaction.SpaceItemDto.SpaceDto.Name,
                    ItemName = transaction.SpaceItemDto.SubProductServiceDto.Name,
                    LinkArea = transaction.SpaceItemDto.SubProductServiceDto.ProductDto.Area,
                    LinkController = transaction.SpaceItemDto.SubProductServiceDto.SystemCode
                });
            }

            return PartialView("_Recent", recents);
        }

    }
}