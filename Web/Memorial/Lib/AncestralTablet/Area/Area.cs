using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.AncestralTablet
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

        public int GetSiteId()
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

        public IEnumerable<Core.Domain.AncestralTabletArea> GetAreaBySite(int siteId)
        {
            return _unitOfWork.AncestralTabletAreas.GetBySite(siteId);
        }

        public IEnumerable<AncestralTabletAreaDto> GetAreaDtosBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletArea>, IEnumerable<AncestralTabletAreaDto>>(GetAreaBySite(siteId));
        }

        public int Create(AncestralTabletAreaDto ancestralTabletAreaDto)
        {
            _area = new Core.Domain.AncestralTabletArea();
            Mapper.Map(ancestralTabletAreaDto, _area);

            _area.CreateDate = DateTime.Now;

            _unitOfWork.AncestralTabletAreas.Add(_area);

            _unitOfWork.Complete();

            return _area.Id;
        }

        public bool Update(AncestralTabletAreaDto ancestralTabletAreaDto)
        {
            var ancestralTabletAreaInDB = GetArea(ancestralTabletAreaDto.Id);

            if (ancestralTabletAreaInDB.SiteId != ancestralTabletAreaDto.SiteDtoId
                && _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == ancestralTabletAreaInDB.SiteId && at.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(ancestralTabletAreaDto, ancestralTabletAreaInDB);

            ancestralTabletAreaInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == id && at.DeleteDate == null).Any())
            {
                return false;
            }

            SetArea(id);

            _area.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}