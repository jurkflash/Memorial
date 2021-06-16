using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Applicant;
using Memorial.Lib.Urn;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;

namespace Memorial.Areas.Urn.Controllers
{
    public class UrnsController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IUrn _urn;
        private readonly IItem _item;
        private readonly ISite _site;
        private readonly ITransaction _transaction;

        public UrnsController(
            IApplicant applicant,
            IUrn urn,
            IItem item,
            ISite site,
            ITransaction transaction)
        {
            _applicant = applicant;
            _urn = urn;
            _item = item;
            _site = site;
            _transaction = transaction;
        }

        public ActionResult Index(byte siteId, int applicantId = 0)
        {
            var viewModel = new UrnIndexesViewModel()
            {
                UrnDtos = _urn.GetUrnDtosBySite(siteId),
                ApplicantId = applicantId
            };
            return View(viewModel);
        }

        public ActionResult Items(int urnId, int applicantId)
        {
            var viewModel = new UrnItemsViewModel()
            {
                UrnItemDtos = _item.GetItemDtosByUrn(urnId),
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
                    CreateDate = transaction.CreateDate,
                    ItemId = transaction.UrnItemDtoId,
                    Text1 = transaction.UrnItemDto.UrnDto.Name,
                    ItemName = transaction.UrnItemDto.SubProductServiceDto.Name,
                    LinkArea = transaction.UrnItemDto.SubProductServiceDto.ProductDto.Area,
                    LinkController = transaction.UrnItemDto.SubProductServiceDto.SystemCode
                });
            }

            return View(recents);
        }
    }
}