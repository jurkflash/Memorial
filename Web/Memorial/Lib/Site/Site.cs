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

        public Core.Domain.Site GetSite(byte id)
        {
            return _unitOfWork.Sites.GetActive(id);
        }

        public SiteDto GetSiteDto(byte id)
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
    }
}