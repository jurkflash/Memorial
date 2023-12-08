using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Memorial.ViewModels;
using Memorial.Core.Dtos;
using Memorial.Lib.Cemetery;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.Site;
using Memorial.Lib;
using PagedList;
using AutoMapper;

namespace Memorial.Areas.Cemetery.Controllers
{
    public class CemeteriesController : Controller
    {
        private readonly IPlot _plot;
        private readonly ISite _site;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly IApplicantDeceased _applicantDeceased;
        private readonly ITransaction _transaction;

        public CemeteriesController(
            IPlot plot,
            ISite site,
            IArea area,
            IItem item,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            ITransaction transaction)
        {
            _plot = plot;
            _site = site;
            _area = area;
            _item = item;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _transaction = transaction;
        }


        public ActionResult Index(byte siteId, int? applicantId)
        {
            var viewModel = new CemeteryAreaIndexesViewModel()
            {
                CemeteryAreaDtos = Mapper.Map<IEnumerable<CemeteryAreaDto>>(_area.GetBySite(siteId)),
                ApplicantId = applicantId,
                SiteDto = Mapper.Map<SiteDto>(_site.Get(siteId))
            };
            return View(viewModel);
        }

        public ActionResult Plots(int areaId, int? applicantId, int? plotTypeId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var viewModel = new PlotIndexesViewModel()
            {
                PlotTypeDtos = Mapper.Map<IEnumerable<PlotTypeDto>>(_plot.GetPlotTypesByAreaId(areaId)),
                CemeteryAreaDto = Mapper.Map<CemeteryAreaDto>(_area.GetById(areaId)),
                SelectedPlotTypeId = plotTypeId,
                ApplicantId = applicantId,
                AreaId = areaId
            };

            if(plotTypeId == null)
            {
                viewModel.PlotDtos = Mapper.Map<IEnumerable<PlotDto>>(_plot.GetByAreaId(areaId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage);
            }
            else
            {
                viewModel.PlotDtos = Mapper.Map<IEnumerable<PlotDto>>(_plot.GetByAreaIdAndTypeId(areaId, (int)plotTypeId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage);
            }

            return View(viewModel);
        }

        public ActionResult Items(int id, int? applicantId)
        {
            var plot = _plot.GetById(id);
            var viewModel = new CemeteryItemsViewModel()
            {
                CemeteryItemDtos = _item.GetItemDtosByPlot(id),
                CemeteryAreaDto = Mapper.Map<CemeteryAreaDto>(_area.GetById(plot.CemeteryAreaId)),
                PlotDto = Mapper.Map<PlotDto>(plot),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult PlotInfo(int id)
        {
            var viewModel = new PlotInfoViewModel();
            var plot = _plot.GetById(id);
            if (plot != null)
            {
                viewModel.PlotDto = Mapper.Map<PlotDto>(plot);
                viewModel.NumberOfPlacements = plot.PlotType.NumberOfPlacement;
                viewModel.CemeteryAreaDto = Mapper.Map<CemeteryAreaDto>(_area.GetById(plot.CemeteryAreaId));
                viewModel.SiteDto = Mapper.Map<SiteDto>(_site.Get(viewModel.CemeteryAreaDto.SiteDtoId));

                if (plot.ApplicantId != null)
                {
                    viewModel.ApplicantDto = Mapper.Map<ApplicantDto>(_applicant.Get((int)plot.ApplicantId));
                    var deceaseds = _deceased.GetByPlotId(plot.Id).ToList();
                    if (deceaseds.Count > 0)
                    {
                        viewModel.DeceasedFlatten1Dto = Mapper.Map<ApplicantDeceasedFlattenDto>(_applicantDeceased.GetApplicantDeceasedFlatten((int)plot.ApplicantId, deceaseds[0].Id));
                    }
                    if (deceaseds.Count > 1)
                    {
                        viewModel.DeceasedFlatten2Dto = Mapper.Map<ApplicantDeceasedFlattenDto>(_applicantDeceased.GetApplicantDeceasedFlatten((int)plot.ApplicantId, deceaseds[1].Id));
                    }
                    if (deceaseds.Count > 2)
                    {
                        viewModel.DeceasedFlatten3Dto = Mapper.Map<ApplicantDeceasedFlattenDto>(_applicantDeceased.GetApplicantDeceasedFlatten((int)plot.ApplicantId, deceaseds[2].Id));
                    }
                }
            }

            return PartialView("_PlotInfo", viewModel);
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
                    ItemId = transaction.CemeteryItemId,
                    Text1 = transaction.Plot.CemeteryArea.Name,
                    Text2 = transaction.Plot.Name,
                    ItemName = transaction.CemeteryItem.SubProductService.Name,
                    LinkArea = transaction.CemeteryItem.SubProductService.Product.Area,
                    LinkController = transaction.CemeteryItem.SubProductService.SystemCode
                });
            }

            return PartialView("_Recent", recents);
        }

    }
}