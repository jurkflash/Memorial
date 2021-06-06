using Memorial.Core;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Site
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISite _site;

        public Config(
            IUnitOfWork unitOfWork,
            ISite site
            )
        {
            _unitOfWork = unitOfWork;
            _site = site;
        }

        public SiteDto GetSiteDto(byte id)
        {
            return _site.GetSiteDto(id);
        }

        public IEnumerable<SiteDto> GetSiteDtos()
        {
            return _site.GetSiteDtos();
        }

        public byte CreateSite(SiteDto siteDto)
        {
            var site = _site.CreateSite(siteDto);

            _unitOfWork.Complete();

            return site.Id;
        }

        public bool UpdateSite(SiteDto siteDto)
        {
            if (_site.UpdateSite(siteDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteSite(byte id)
        {
            if (_site.DeleteSite(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }


    }
}