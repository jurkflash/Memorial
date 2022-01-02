﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Cemetery
{
    public class Area : IArea
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.CemeteryArea _area;

        public Area(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetArea(int id)
        {
            _area = _unitOfWork.CemeteryAreas.GetActive(id);
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

        public Core.Domain.CemeteryArea GetArea()
        {
            return _area;
        }

        public CemeteryAreaDto GetAreaDto()
        {
            return Mapper.Map<Core.Domain.CemeteryArea, CemeteryAreaDto>(_area);
        }

        public Core.Domain.CemeteryArea GetArea(int areaId)
        {
            return _unitOfWork.CemeteryAreas.GetActive(areaId);
        }

        public CemeteryAreaDto GetAreaDto(int areaId)
        {
            return Mapper.Map<Core.Domain.CemeteryArea, CemeteryAreaDto>(GetArea(areaId));
        }

        public IEnumerable<CemeteryAreaDto> GetAreaDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.CemeteryArea>, IEnumerable<CemeteryAreaDto>>(_unitOfWork.CemeteryAreas.GetAllActive());
        }

        public IEnumerable<Core.Domain.CemeteryArea> GetAreaBySite(int siteId)
        {
            return _unitOfWork.CemeteryAreas.GetBySite(siteId);
        }

        public IEnumerable<CemeteryAreaDto> GetAreaDtosBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CemeteryArea>, IEnumerable<CemeteryAreaDto>>(GetAreaBySite(siteId));
        }

        public int Create(CemeteryAreaDto cemeteryAreaDto)
        {
            _area = new Core.Domain.CemeteryArea();
            Mapper.Map(cemeteryAreaDto, _area);

            _unitOfWork.CemeteryAreas.Add(_area);

            _unitOfWork.Complete();

            return _area.Id;
        }

        public bool Update(CemeteryAreaDto cemeteryAreaDto)
        {
            var cemeteryAreaInDB = GetArea(cemeteryAreaDto.Id);

            if (cemeteryAreaInDB.SiteId != cemeteryAreaDto.SiteDtoId
                && _unitOfWork.CemeteryTransactions.Find(ct => ct.Plot.CemeteryAreaId == cemeteryAreaInDB.Id).Any())
            {
                return false;
            }

            Mapper.Map(cemeteryAreaDto, cemeteryAreaInDB);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(ct => ct.Plot.CemeteryAreaId == id).Any())
            {
                return false;
            }

            SetArea(id);

            if (_area == null)
            {
                return false;
            }

            _unitOfWork.CemeteryAreas.Remove(_area);

            _unitOfWork.Complete();

            return true;
        }
    }
}