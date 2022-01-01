using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Site
{
    public class Site : ISite
    {
        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.Site _site;

        public Site(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetSite(int id)
        {
            _site = _unitOfWork.Sites.GetActive(id);
        }

        public Core.Domain.Site GetSite()
        {
            return _site;
        }

        public SiteDto GetSiteDto()
        {
            return Mapper.Map<Core.Domain.Site, SiteDto>(_site);
        }

        public Core.Domain.Site GetSite(int id)
        {
            return _unitOfWork.Sites.GetActive(id);
        }

        public SiteDto GetSiteDto(int id)
        {
            return Mapper.Map<Core.Domain.Site, SiteDto>(GetSite(id));
        }

        public IEnumerable<Core.Domain.Site> GetSites()
        {
            return _unitOfWork.Sites.GetAllActive();
        }

        public IEnumerable<SiteDto> GetSiteDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.Site>, IEnumerable<SiteDto>>(GetSites());
        }

        public int CreateSite(SiteDto siteDto)
        {
            _site = new Core.Domain.Site();
            Mapper.Map(siteDto, _site);

            _site.CreatedDate = DateTime.Now;

            _unitOfWork.Sites.Add(_site);

            _unitOfWork.Complete();

            return _site.Id;
        }

        public bool UpdateSite(SiteDto siteDto)
        {
            var siteInDB = GetSite(siteDto.Id);

            Mapper.Map(siteDto, siteInDB);

            siteInDB.ModifiedDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool DeleteSite(int id)
        {
            if (
                _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == id && at.DeletedDate == null).Any() ||
                _unitOfWork.CremationTransactions.Find(at => at.CremationItem.Cremation.SiteId == id && at.DeleteDate == null).Any() ||
                _unitOfWork.MiscellaneousTransactions.Find(at => at.MiscellaneousItem.Miscellaneous.SiteId == id && at.DeleteDate == null).Any() ||
                _unitOfWork.CemeteryTransactions.Find(at => at.Plot.CemeteryArea.SiteId == id && at.DeletedDate == null).Any() ||
                _unitOfWork.ColumbariumTransactions.Find(at => at.ColumbariumItem.ColumbariumCentre.SiteId == id && at.DeletedDate == null).Any() ||
                _unitOfWork.SpaceTransactions.Find(at => at.SpaceItem.Space.SiteId == id && at.DeletedDate == null).Any() ||
                _unitOfWork.UrnTransactions.Find(at => at.UrnItem.Urn.SiteId == id && at.DeletedDate == null).Any())
            {
                return false;
            }

            SetSite(id);

            _site.DeletedDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}