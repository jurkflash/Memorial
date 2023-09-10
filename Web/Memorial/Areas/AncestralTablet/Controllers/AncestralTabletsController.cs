using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Core.Dtos;
using Memorial.Lib.Site;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.AncestralTablet;
using AutoMapper;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    [Authorize]
    public class AncestralTabletsController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly ISite _site;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IDeceased _deceased;
        private readonly IApplicantDeceased _applicantDeceased;
        private readonly ITransaction _transaction;

        public AncestralTabletsController(
            IAncestralTablet ancestralTablet,
            ISite site,
            IArea area,
            IItem item,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            ITransaction transaction)
        {
            _ancestralTablet = ancestralTablet;
            _site = site;
            _area = area;
            _item = item;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _transaction = transaction;
        }

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new AncestralTabletAreaIndexesViewModel()
            {
                AncestralTabletAreaDtos = Mapper.Map<IEnumerable<AncestralTabletAreaDto>>(_area.GetBySite(siteId)),
                ApplicantId = applicantId,
                SiteDto = Mapper.Map<SiteDto>(_site.Get(siteId))
            };
            return View(viewModel);
        }

        public ActionResult AncestralTablets(int areaId, int applicantId, byte siteId)
        {
            var viewModel = new AncestralTabletIndexesViewModel()
            {
                AncestralTabletDtos = Mapper.Map<IEnumerable<AncestralTabletDto>>(_ancestralTablet.GetByAreaId(areaId)),
                Positions = _ancestralTablet.GetPositionsByAreaId(areaId),
                ApplicantId = applicantId,
                SiteDto = Mapper.Map<SiteDto>(_site.Get(siteId))
            };
            return View(viewModel);
        }

        public ActionResult Items(int id, int applicantId)
        {
            var ancestralTablet = _ancestralTablet.GetById(id);
            var viewModel = new AncestralTabletItemsViewModel()
            {
                AncestralTabletItemDtos = Mapper.Map<IEnumerable<AncestralTabletItemDto>>(_item.GetByArea(ancestralTablet.AncestralTabletAreaId)),
                AncestralTabletDto = Mapper.Map<AncestralTabletDto>(ancestralTablet),
                ApplicantId = applicantId,
                SiteDto = Mapper.Map<SiteDto>(ancestralTablet.AncestralTabletArea.Site)
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult AncestralTabletInfo(int id)
        {
            var viewModel = new AncestralTabletInfoViewModel();
            var ancestralTablet = _ancestralTablet.GetById(id); 
            if (ancestralTablet != null)
            {
                viewModel.AncestralTabletDto = Mapper.Map<AncestralTabletDto>(ancestralTablet);
                viewModel.AncestralTabletAreaDto = Mapper.Map<AncestralTabletAreaDto>(ancestralTablet.AncestralTabletArea);
                viewModel.SiteDto = Mapper.Map<SiteDto>(ancestralTablet.AncestralTabletArea.Site);

                if (ancestralTablet.ApplicantId != null)
                {
                    viewModel.ApplicantDto = Mapper.Map<ApplicantDto>(ancestralTablet.Applicant);
                    var deceaseds = _deceased.GetByAncestralTabletId(id).ToList();
                    if (deceaseds.Count > 0)
                    {
                        viewModel.DeceasedFlattenDto = Mapper.Map<ApplicantDeceasedFlattenDto>(_applicantDeceased.GetApplicantDeceasedFlatten((int)ancestralTablet.ApplicantId, deceaseds[0].Id));
                    }
                }
            }

            return PartialView("_AncestralTabletInfo", viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult Recent(int siteId, int? applicantId)
        {
            List<RecentDto> recents = new List<RecentDto>();

            var transactions = _transaction.GetRecent(siteId, applicantId);

            foreach (var transaction in transactions)
            {
                recents.Add(new RecentDto()
                {
                    Code = transaction.AF,
                    ApplicantName = transaction.Applicant.Name,
                    CreatedDate = transaction.CreatedUtcTime,
                    ItemId = transaction.AncestralTabletItemId,
                    Text1 = transaction.AncestralTablet.AncestralTabletArea.Name,
                    Text2 = transaction.AncestralTablet.Name,
                    ItemName = transaction.AncestralTabletItem.SubProductService.Name,
                    LinkArea = transaction.AncestralTabletItem.SubProductService.Product.Area,
                    LinkController = transaction.AncestralTabletItem.SubProductService.SystemCode
                });
            }

            return PartialView("_Recent", recents);
        }
    }
}