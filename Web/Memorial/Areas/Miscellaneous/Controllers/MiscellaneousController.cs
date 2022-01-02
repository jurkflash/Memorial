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

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new MiscellaneousIndexesViewModel()
            {
                MiscellaneousDtos = _miscellaneous.GetMiscellaneousDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int miscellaneousId, int applicantId)
        {
            var viewModel = new MiscellaneousItemsViewModel()
            {
                MiscellaneousItemDtos = _item.GetItemDtosByMiscellaneous(miscellaneousId),
                ApplicantId = applicantId
            };
            return View(viewModel);
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
                    ItemId = transaction.MiscellaneousItemDtoId,
                    Text1 = transaction.MiscellaneousItemDto.MiscellaneousDto.Name,
                    ItemName = transaction.MiscellaneousItemDto.SubProductServiceDto.Name,
                    LinkArea = transaction.MiscellaneousItemDto.SubProductServiceDto.ProductDto.Area,
                    LinkController = transaction.MiscellaneousItemDto.SubProductServiceDto.SystemCode
                });
            }

            return View(recents);
        }

    }
}