using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.Lib.Applicant;
using Memorial.Lib.Cremation;
using Memorial.Lib.Site;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Cremation.Controllers
{
    public class CremationsController : Controller
    {
        private readonly ICremation _cremation;
        private readonly IItem _item;
        private readonly ISite _site;
        private readonly ITransaction _transaction;

        public CremationsController(
            ICremation cremation,
            IItem item,
            ISite site,
            ITransaction transaction)
        {
            _cremation = cremation;
            _item = item;
            _site = site;
            _transaction = transaction;
        }

        public ActionResult Index(byte siteId, int? applicantId)
        {
            var viewModel = new CremationIndexesViewModel();
            viewModel.CremationDtos = Mapper.Map<IEnumerable<CremationDto>>(_cremation.GetBySite(siteId));
            viewModel.SiteDto = Mapper.Map<SiteDto>(_site.Get(siteId));

            if (applicantId == null)
            {
                viewModel.ApplicantId = applicantId;
            }

            return View(viewModel);
        }

        public ActionResult Items(int cremationId, int applicantId)
        {
            var cremation = _cremation.GetById(cremationId);
            var viewModel = new CremationItemsViewModel()
            {
                CremationItemDtos = Mapper.Map<IEnumerable<CremationItemDto>>(_item.GetByCremation(cremationId)),
                ApplicantId = applicantId,
                SiteDto = Mapper.Map<SiteDto>(cremation.Site)
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
                    ApplicantName = transaction.Applicant.Name,
                    CreatedDate = transaction.CreatedUtcTime,
                    ItemId = transaction.CremationItemId,
                    Text1 = transaction.CremationItem.Cremation.Name,
                    ItemName = transaction.CremationItem.SubProductService.Name,
                    LinkArea = transaction.CremationItem.SubProductService.Product.Area,
                    LinkController = transaction.CremationItem.SubProductService.SystemCode
                });
            }

            return PartialView("_Recent", recents);
        }
    }
}