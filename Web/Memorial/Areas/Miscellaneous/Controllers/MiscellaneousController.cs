using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Applicant;
using Memorial.Lib.Miscellaneous;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Miscellaneous.Controllers
{
    public class MiscellaneousController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IMiscellaneous _miscellaneous;
        private readonly IItem _item;
        private readonly ISite _site;
        private readonly ITransaction _transaction;

        public MiscellaneousController(
            IApplicant applicant,
            IMiscellaneous miscellaneous,
            IItem item,
            ISite site,
            ITransaction transaction)
        {
            _applicant = applicant;
            _miscellaneous = miscellaneous;
            _item = item;
            _site = site;
            _transaction = transaction;
        }

        public ActionResult Index(byte siteId, int? applicantId)
        {
            var viewModel = new MiscellaneousIndexesViewModel();
            viewModel.MiscellaneousDtos = Mapper.Map<IEnumerable<MiscellaneousDto>>(_miscellaneous.GetBySite(siteId));
            viewModel.SiteDto = Mapper.Map<SiteDto>(_site.Get(siteId));

            if (applicantId != null)
            {
                viewModel.ApplicantId = applicantId;
            }
            
            return View(viewModel);
        }

        public ActionResult Items(int miscellaneousId, int applicantId)
        {
            var viewModel = new MiscellaneousItemsViewModel()
            {
                MiscellaneousItemDtos = Mapper.Map<IEnumerable<MiscellaneousItemDto>>(_item.GetByMiscellaneous(miscellaneousId)),
                ApplicantId = applicantId,
                SiteDto = Mapper.Map<SiteDto>(_miscellaneous.Get(miscellaneousId).Site)
            };
            return View(viewModel);
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
                    ItemId = transaction.MiscellaneousItemId,
                    Text1 = transaction.MiscellaneousItem.Miscellaneous.Name,
                    ItemName = transaction.MiscellaneousItem.SubProductService.Name,
                    LinkArea = transaction.MiscellaneousItem.SubProductService.Product.Area,
                    LinkController = transaction.MiscellaneousItem.SubProductService.SystemCode
                });
            }

            return PartialView("_Recent", recents);
        }

    }
}