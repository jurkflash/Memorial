using System;
using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Ancestor
{
    public class Area : IArea
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.AncestralTabletArea _area;

        public Area(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetArea(int id)
        {
            _area = _unitOfWork.AncestralTabletAreas.GetActive(id);
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

        public Core.Domain.AncestralTabletArea GetArea()
        {
            return _area;
        }

        public AncestralTabletAreaDto GetAreaDto()
        {
            return Mapper.Map<Core.Domain.AncestralTabletArea, AncestralTabletAreaDto>(_area);
        }

        public Core.Domain.AncestralTabletArea GetArea(int areaId)
        {
            return _unitOfWork.AncestralTabletAreas.GetActive(areaId);
        }

        public AncestralTabletAreaDto GetAreaDto(int areaId)
        {
            return Mapper.Map<Core.Domain.AncestralTabletArea, AncestralTabletAreaDto>(GetArea(areaId));
        }

        public IEnumerable<Core.Domain.AncestralTabletArea> GetAreas()
        {
            return _unitOfWork.AncestralTabletAreas.GetAllActive();
        }

        public IEnumerable<AncestralTabletAreaDto> GetAreaDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletArea>, IEnumerable<AncestralTabletAreaDto>>(GetAreas());
        }

        public IEnumerable<Core.Domain.AncestralTabletArea> GetAreaBySite(byte siteId)
        {
            return _unitOfWork.AncestralTabletAreas.GetBySite(siteId);
        }

        public IEnumerable<AncestralTabletAreaDto> GetAreaDtosBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletArea>, IEnumerable<AncestralTabletAreaDto>>(GetAreaBySite(siteId));
        }

        public bool Create(AncestralTabletAreaDto ancestralTabletAreaDto)
        {
            _area = new Core.Domain.AncestralTabletArea();
            Mapper.Map(ancestralTabletAreaDto, _area);

            _area.CreateDate = DateTime.Now;

            _unitOfWork.AncestralTabletAreas.Add(_area);

            return true;
        }

        public bool Update(Core.Domain.AncestralTabletArea ancestralTabletArea)
        {
            ancestralTabletArea.ModifyDate = DateTime.Now;

            return true;
        }

        public bool Delete(int id)
        {
            SetArea(id);

            _area.DeleteDate = DateTime.Now;

            return true;
        }
    }
}