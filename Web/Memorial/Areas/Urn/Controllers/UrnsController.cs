using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.Lib.Urn;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Urn.Controllers
{
    public class UrnsController : Controller
    {
        private readonly IUrn _urn;
        private readonly IItem _item;
        private readonly ITransaction _transaction;

        public UrnsController(
            IUrn urn,
            IItem item,
            ITransaction transaction)
        {
            _urn = urn;
            _item = item;
            _transaction = transaction;
        }

        public ActionResult Index(byte siteId, int? applicantId)
        {
            var viewModel = new UrnIndexesViewModel();
            viewModel.UrnDtos = Mapper.Map<IEnumerable<UrnDto>>(_urn.GetBySite(siteId));

            if (applicantId != null)
            { 
                viewModel.ApplicantId = applicantId;
            };

            return View(viewModel);
        }

        public ActionResult Items(int urnId, int applicantId)
        {
            var viewModel = new UrnItemsViewModel()
            {
                UrnItemDtos = Mapper.Map<IEnumerable<UrnItemDto>>(_item.GetByUrn(urnId)),
                ApplicantId = applicantId,
                SiteDto = Mapper.Map<SiteDto>(_urn.Get(urnId).Site)
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
                    ItemId = transaction.UrnItemId,
                    Text1 = transaction.UrnItem.Urn.Name,
                    ItemName = transaction.UrnItem.SubProductService.Name,
                    LinkArea = transaction.UrnItem.SubProductService.Product.Area,
                    LinkController = transaction.UrnItem.SubProductService.SystemCode
                });
            }

            return PartialView("_Recent", recents);
        }
    }
}