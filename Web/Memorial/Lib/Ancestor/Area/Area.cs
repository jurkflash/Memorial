using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Ancestor
{
    public class Area : IArea
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.AncestorArea _area;

        public Area(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetArea(int id)
        {
            _area = _unitOfWork.AncestorAreas.GetActive(id);
        }

        public int GetId()
        {
            return _area.Id;
        }

        public string GetName()
        {
            return _area.Name;
        }

        public string GetDescription()
        {
            return _area.Description;
        }

        public byte GetSiteId()
        {
            return _area.SiteId;
        }

        public Core.Domain.AncestorArea GetArea()
        {
            return _area;
        }

        public AncestorAreaDto GetAreaDto()
        {
            return Mapper.Map<Core.Domain.AncestorArea, AncestorAreaDto>(_area);
        }

        public Core.Domain.AncestorArea GetArea(int areaId)
        {
            return _unitOfWork.AncestorAreas.GetActive(areaId);
        }

        public AncestorAreaDto GetAreaDto(int areaId)
        {
            return Mapper.Map<Core.Domain.AncestorArea, AncestorAreaDto>(GetArea(areaId));
        }

        public IEnumerable<Core.Domain.AncestorArea> GetAreaBySite(byte siteId)
        {
            return _unitOfWork.AncestorAreas.GetBySite(siteId);
        }

        public IEnumerable<AncestorAreaDto> GetAreaDtosBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestorArea>, IEnumerable<AncestorAreaDto>>(GetAreaBySite(siteId));
        }
    }
}