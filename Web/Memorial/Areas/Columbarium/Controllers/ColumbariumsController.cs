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

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            return Centre(siteId, applicantId);
        }

        public ActionResult Centre(byte siteId, int applicantId)
        {
            var viewModel = new ColumbariumCentreIndexesViewModel()
            {
                ColumbariumCentreDtos = _centre.GetCentreDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View("Centre", viewModel);
        }

        public ActionResult Area(int centreId, int applicantId)
        {
            var viewModel = new ColumbariumAreaIndexesViewModel()
            {
                ColumbariumAreaDtos = _area.GetAreaDtosByCentre(centreId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Niches(int areaId, int applicantId)
        {
            var viewModel = new NicheIndexesViewModel()
            {
                NicheDtos = _niche.GetNicheDtosByAreaId(areaId),
                Positions = _niche.GetPositionsByAreaId(areaId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int id, int applicantId)
        {
            _niche.SetNiche(id);
            _area.SetArea(_niche.GetAreaId());
            _centre.SetCentre(_area.GetCentreId());
            var viewModel = new ColumbariumItemsViewModel()
            {
                ColumbariumItemDtos = _item.GetItemDtosByCentre(_centre.GetID()),
                NicheDto = _niche.GetNicheDto(),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult NicheInfo(int id)
        {
            var viewModel = new ColumbariumInfoViewModel();
            _niche.SetNiche(id);
            
            if(_niche.GetNicheDto() != null)
            {
                viewModel.NicheDto = _niche.GetNicheDto();
                viewModel.NumberOfPlacements = _niche.GetNumberOfPlacement();
                viewModel.ColumbariumAreaDto = _area.GetAreaDto(_niche.GetNicheDto().ColumbariumAreaDtoId);
                viewModel.ColumbariumCentreDto = _centre.GetCentreDto(viewModel.ColumbariumAreaDto.ColumbariumCentreDtoId);
                viewModel.SiteDto = _site.GetSiteDto(viewModel.ColumbariumCentreDto.SiteDtoId);

                if(_niche.HasApplicant())
                {
                    viewModel.ApplicantDto = _applicant.GetApplicantDto((int)_niche.GetApplicantId());
                    var deceaseds = _deceased.GetDeceasedsByNicheId(_niche.GetNiche().Id).ToList();
                    if(deceaseds.Count > 0)
                    {
                        viewModel.DeceasedFlatten1Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_niche.GetApplicantId(), deceaseds[0].Id);
                    }
                    if (deceaseds.Count > 1)
                    {
                        viewModel.DeceasedFlatten2Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_niche.GetApplicantId(), deceaseds[1].Id);
                    }
                }
            }

            return PartialView("_NicheInfo", viewModel);
        }

        public ActionResult Menu(int siteId)
        {
            List<RecentDto> recents = new List<RecentDto>();

            var transactions = _transaction.GetRecent(null, siteId);

            foreach (var transaction in transactions)
            {
                recents.Add(new RecentDto()
                {
                    Code = transaction.AF,
                    ApplicantName = transaction.ApplicantDto.Name,
                    CreatedDate = transaction.CreatedDate,
                    ItemId = transaction.ColumbariumItemDtoId,
                    Text1 = transaction.NicheDto.ColumbariumAreaDto.ColumbariumCentreDto.Name,
                    Text2 = transaction.NicheDto.ColumbariumAreaDto.Name,
                    Text3 = transaction.NicheDto.Name,
                    ItemName = transaction.ColumbariumItemDto.SubProductServiceDto.Name,
                    LinkArea = transaction.ColumbariumItemDto.SubProductServiceDto.ProductDto.Area,
                    LinkController = transaction.ColumbariumItemDto.SubProductServiceDto.SystemCode
                });
            }

            return View(recents);
        }
    }
}