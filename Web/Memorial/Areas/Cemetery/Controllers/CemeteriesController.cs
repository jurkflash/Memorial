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


        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new CemeteryAreaIndexesViewModel()
            {
                CemeteryAreaDtos = _area.GetAreaDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Plots(int areaId, int applicantId, int? plotTypeId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var viewModel = new PlotIndexesViewModel()
            {
                PlotTypeDtos = _plot.GetPlotTypeDtosByAreaId(areaId),
                SelectedPlotTypeId = plotTypeId,
                ApplicantId = applicantId,
                AreaId = areaId
            };

            if(plotTypeId == null)
            {
                viewModel.PlotDtos = _plot.GetPlotDtosByAreaId(areaId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage);
            }
            else
            {
                viewModel.PlotDtos = _plot.GetPlotDtosByAreaIdAndTypeId(areaId, (int)plotTypeId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage);
            }

            return View(viewModel);
        }

        public ActionResult Items(int id, int applicantId)
        {
            _plot.SetPlot(id);
            var viewModel = new CemeteryItemsViewModel()
            {
                CemeteryItemDtos = _item.GetItemDtosByPlot(id),
                PlotDto = _plot.GetPlotDto(),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult PlotInfo(int id)
        {
            var viewModel = new PlotInfoViewModel();
            _plot.SetPlot(id);

            if (_plot.GetPlotDto() != null)
            {
                viewModel.PlotDto = _plot.GetPlotDto();
                viewModel.NumberOfPlacements = _plot.GetNumberOfPlacement();
                viewModel.CemeteryAreaDto = _area.GetAreaDto(_plot.GetAreaId());
                viewModel.SiteDto = _site.GetSiteDto(viewModel.CemeteryAreaDto.SiteDtoId);

                if (_plot.HasApplicant())
                {
                    viewModel.ApplicantDto = _applicant.GetApplicantDto((int)_plot.GetApplicantId());
                    var deceaseds = _deceased.GetDeceasedsByPlotId(_plot.GetPlot().Id).ToList();
                    if (deceaseds.Count > 0)
                    {
                        viewModel.DeceasedFlatten1Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_plot.GetApplicantId(), deceaseds[0].Id);
                    }
                    if (deceaseds.Count > 1)
                    {
                        viewModel.DeceasedFlatten2Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_plot.GetApplicantId(), deceaseds[1].Id);
                    }
                    if (deceaseds.Count > 2)
                    {
                        viewModel.DeceasedFlatten3Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_plot.GetApplicantId(), deceaseds[2].Id);
                    }
                }
            }

            return PartialView("_PlotInfo", viewModel);
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
                    ItemId = transaction.CemeteryItemDtoId,
                    Text1 = transaction.PlotDto.CemeteryAreaDto.Name,
                    Text2 = transaction.PlotDto.Name,
                    ItemName = transaction.CemeteryItemDto.SubProductServiceDto.Name,
                    LinkArea = transaction.CemeteryItemDto.SubProductServiceDto.ProductDto.Area,
                    LinkController = transaction.CemeteryItemDto.SubProductServiceDto.SystemCode
                });
            }

            return View(recents);
        }

    }
}