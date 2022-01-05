using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Core.Dtos;
using Memorial.Lib.Site;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.AncestralTablet;
using Memorial.Lib;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class AncestralTabletsController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly ISite _site;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly IApplicantDeceased _applicantDeceased;
        private readonly ITransaction _transaction;

        public AncestralTabletsController(
            IAncestralTablet ancestralTablet,
            ISite site,
            IArea area,
            IItem item,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            ITransaction transaction)
        {
            _ancestralTablet = ancestralTablet;
            _site = site;
            _area = area;
            _item = item;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _transaction = transaction;
        }

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new AncestralTabletAreaIndexesViewModel()
            {
                AncestralTabletAreaDtos = _area.GetAreaDtosBySite(siteId),
                ApplicantId = applicantId,
                SiteDto = _site.GetSiteDto(siteId)
            };
            return View(viewModel);
        }

        public ActionResult AncestralTablets(int areaId, int applicantId, byte siteId)
        {
            var viewModel = new AncestralTabletIndexesViewModel()
            {
                AncestralTabletDtos = _ancestralTablet.GetAncestralTabletDtosByAreaId(areaId),
                Positions = _ancestralTablet.GetPositionsByAreaId(areaId),
                ApplicantId = applicantId,
                SiteDto = _site.GetSiteDto(siteId)
            };
            return View(viewModel);
        }

        public ActionResult Items(int id, int applicantId)
        {
            _ancestralTablet.SetAncestralTablet(id);
            _area.SetArea(_ancestralTablet.GetAreaId());
            var viewModel = new AncestralTabletItemsViewModel()
            {
                AncestralTabletItemDtos = _item.GetItemDtosByArea(_area.GetId()),
                AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto(),
                ApplicantId = applicantId,
                SiteDto = _ancestralTablet.GetAncestralTabletDto().AncestralTabletAreaDto.SiteDto
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult AncestralTabletInfo(int id)
        {
            var viewModel = new AncestralTabletInfoViewModel();
            _ancestralTablet.SetAncestralTablet(id);

            if (_ancestralTablet.GetAncestralTabletDto() != null)
            {
                viewModel.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();
                viewModel.AncestralTabletAreaDto = _area.GetAreaDto(_ancestralTablet.GetAreaId());
                viewModel.SiteDto = _site.GetSiteDto(viewModel.AncestralTabletAreaDto.SiteDtoId);

                if (_ancestralTablet.HasApplicant())
                {
                    viewModel.ApplicantDto = _applicant.GetApplicantDto((int)_ancestralTablet.GetApplicantId());
                    var deceaseds = _deceased.GetDeceasedsByAncestralTabletId(_ancestralTablet.GetAncestralTablet().Id).ToList();
                    if (deceaseds.Count > 0)
                    {
                        viewModel.DeceasedFlattenDto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_ancestralTablet.GetApplicantId(), deceaseds[0].Id);
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
                    ApplicantName = transaction.ApplicantDto.Name,
                    CreatedDate = transaction.CreatedDate,
                    ItemId = transaction.AncestralTabletItemDtoId,
                    Text1 = transaction.AncestralTabletDto.AncestralTabletAreaDto.Name,
                    Text2 = transaction.AncestralTabletDto.Name,
                    ItemName = transaction.AncestralTabletItemDto.SubProductServiceDto.Name,
                    LinkArea = transaction.AncestralTabletItemDto.SubProductServiceDto.ProductDto.Area,
                    LinkController = transaction.AncestralTabletItemDto.SubProductServiceDto.SystemCode
                });
            }

            return PartialView("_Recent", recents);
        }
    }
}