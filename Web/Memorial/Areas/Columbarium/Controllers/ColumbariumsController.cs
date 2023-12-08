using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Memorial.ViewModels;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Site;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class ColumbariumsController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly IApplicantDeceased _applicantDeceased;
        private readonly INiche _niche;
        private readonly IArea _area;
        private readonly ICentre _centre;
        private readonly IItem _item;
        private readonly ISite _site;
        private readonly ITransaction _transaction;

        public ColumbariumsController(
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased, 
            INiche niche, 
            ICentre centre, 
            IArea area, 
            IItem item,
            ISite site,
            ITransaction transaction)
        {
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _niche = niche;
            _area = area;
            _centre = centre;
            _item = item;
            _site = site;
            _transaction = transaction;
        }

        public ActionResult Index(byte siteId, int? applicantId)
        {
            return Centre(siteId, applicantId);
        }

        public ActionResult Centre(byte siteId, int? applicantId)
        {
            var viewModel = new ColumbariumCentreIndexesViewModel();
            viewModel.ColumbariumCentreDtos = Mapper.Map<IEnumerable<ColumbariumCentreDto>>(_centre.GetBySite(siteId));
            viewModel.SiteDto = Mapper.Map<SiteDto>(_site.Get(siteId));

            if(applicantId != null)
            {
                viewModel.ApplicantId = applicantId;
            };
            return View("Centre", viewModel);
        }

        public ActionResult Area(int centreId, int? applicantId)
        {
            var viewModel = new ColumbariumAreaIndexesViewModel();
            viewModel.ColumbariumAreaDtos = Mapper.Map<IEnumerable<ColumbariumAreaDto>>(_area.GetByCentre(centreId));
            viewModel.ColumbariumCentreDto = Mapper.Map<ColumbariumCentreDto>(_centre.GetById(centreId));

            if (applicantId != null)
            {
                viewModel.ApplicantId = applicantId;
            };
            return View(viewModel);
        }

        public ActionResult Niches(int areaId, int? applicantId)
        {
            var area = _area.GetById(areaId);

            var viewModel = new NicheIndexesViewModel();
            viewModel.ColumbariumCentreDto = Mapper.Map<ColumbariumCentreDto>(area.ColumbariumCentre);
            viewModel.ColumbariumAreaDto = Mapper.Map<ColumbariumAreaDto>(area);
            viewModel.NicheDtos = Mapper.Map<IEnumerable<NicheDto>>(_niche.GetByAreaId(areaId));
            viewModel.Positions = _niche.GetPositionsByAreaId(areaId);

            if (applicantId != null)
            {
                viewModel.ApplicantId = applicantId;
            };
            return View(viewModel);
        }

        public ActionResult Items(int id, int? applicantId)
        {
            var niche = _niche.GetById(id);
            var area = _area.GetById(niche.ColumbariumAreaId);
            var viewModel = new ColumbariumItemsViewModel();
            viewModel.ColumbariumCentreDto = Mapper.Map<ColumbariumCentreDto>(area.ColumbariumCentre);
            viewModel.ColumbariumItemDtos = Mapper.Map<IEnumerable<ColumbariumItemDto>>(_item.GetByCentre(area.ColumbariumCentreId));
            viewModel.NicheDto = Mapper.Map<NicheDto>(niche);

            if (applicantId != null)
            {
                viewModel.ApplicantId = applicantId;
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult NicheInfo(int id)
        {
            var viewModel = new ColumbariumInfoViewModel();
            var niche = _niche.GetById(id);
            if(niche != null)
            {
                viewModel.NicheDto = Mapper.Map<NicheDto>(niche);
                viewModel.NumberOfPlacements = niche.NicheType.NumberOfPlacement;
                viewModel.ColumbariumAreaDto = Mapper.Map<ColumbariumAreaDto>(niche.ColumbariumArea);
                viewModel.ColumbariumCentreDto = Mapper.Map<ColumbariumCentreDto>(_centre.GetById(niche.ColumbariumAreaId));
                viewModel.SiteDto = Mapper.Map<SiteDto>(viewModel.ColumbariumCentreDto.SiteDto);

                if(niche.ApplicantId != null)
                {
                    viewModel.ApplicantDto = Mapper.Map<ApplicantDto>(niche.Applicant);
                    var deceaseds = _deceased.GetByNicheId(niche.Id).ToList();
                    if(deceaseds.Count > 0)
                    {
                        viewModel.DeceasedFlatten1Dto = Mapper.Map<ApplicantDeceasedFlattenDto>(_applicantDeceased.GetApplicantDeceasedFlatten((int)niche.ApplicantId, deceaseds[0].Id));
                    }
                    if (deceaseds.Count > 1)
                    {
                        viewModel.DeceasedFlatten2Dto = Mapper.Map<ApplicantDeceasedFlattenDto>(_applicantDeceased.GetApplicantDeceasedFlatten((int)niche.ApplicantId, deceaseds[1].Id));
                    }
                }
            }

            return PartialView("_NicheInfo", viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult Recent(byte? siteId, int? applicantId)
        {
            List<RecentDto> recents = new List<RecentDto>();

            var transactions = _transaction.GetRecent(siteId, applicantId);

            foreach (var transaction in transactions)
            {
                recents.Add(new RecentDto()
                {
                    AF = transaction.AF,
                    ApplicantName = transaction.Applicant.Name,
                    CreatedDate = transaction.CreatedUtcTime,
                    ItemId = transaction.ColumbariumItemId,
                    Text1 = transaction.Niche.ColumbariumArea.ColumbariumCentre.Name,
                    Text2 = transaction.Niche.ColumbariumArea.Name,
                    Text3 = transaction.Niche.Name,
                    ItemName = transaction.ColumbariumItem.SubProductService.Name,
                    LinkArea = transaction.ColumbariumItem.SubProductService.Product.Area,
                    LinkController = transaction.ColumbariumItem.SubProductService.SystemCode
                });
            }

            return PartialView("_Recent", recents);
        }
    }
}