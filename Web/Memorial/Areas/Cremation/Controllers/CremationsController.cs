using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Applicant;
using Memorial.Lib.Cremation;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;

namespace Memorial.Areas.Cremation.Controllers
{
    public class CremationsController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly ICremation _cremation;
        private readonly IItem _item;
        private readonly ISite _site;
        private readonly ITransaction _transaction;

        public CremationsController(
            IApplicant applicant,
            ICremation cremation,
            IItem item,
            ISite site,
            ITransaction transaction)
        {
            _applicant = applicant;
            _cremation = cremation;
            _item = item;
            _site = site;
            _transaction = transaction;
        }

        public ActionResult Index(byte siteId, int? applicantId)
        {
            var viewModel = new CremationIndexesViewModel();
            viewModel.CremationDtos = _cremation.GetCremationDtosBySite(siteId);
            viewModel.SiteDto = _site.GetSiteDto(siteId);

            if (applicantId == null)
            {
                viewModel.ApplicantId = applicantId;
            }

            return View(viewModel);
        }

        public ActionResult Items(int cremationId, int applicantId)
        {
            var viewModel = new CremationItemsViewModel()
            {
                CremationItemDtos = _item.GetItemDtosByCremation(cremationId),
                ApplicantId = applicantId,
                SiteDto = _cremation.GetCremationDto(cremationId).SiteDto
            };
            return View(viewModel);
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
                    CreatedDate = transaction.CreatedUtcTime,
                    ItemId = transaction.CremationItemDtoId,
                    Text1 = transaction.CremationItemDto.CremationDto.Name,
                    ItemName = transaction.CremationItemDto.SubProductServiceDto.Name,
                    LinkArea = transaction.CremationItemDto.SubProductServiceDto.ProductDto.Area,
                    LinkController = transaction.CremationItemDto.SubProductServiceDto.SystemCode
                });
            }

            return PartialView("_Recent", recents);
        }
    }
}