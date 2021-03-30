using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Plot
{
    public class Area : IArea
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.PlotArea _area;

        public Area(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetArea(int id)
        {
            _area = _unitOfWork.PlotAreas.GetActive(id);
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

        public Core.Domain.PlotArea GetArea()
        {
            return _area;
        }

        public PlotAreaDto GetAreaDto()
        {
            return Mapper.Map<Core.Domain.PlotArea, PlotAreaDto>(_area);
        }

        public Core.Domain.PlotArea GetArea(int areaId)
        {
            return _unitOfWork.PlotAreas.GetActive(areaId);
        }

        public PlotAreaDto GetAreaDto(int areaId)
        {
            return Mapper.Map<Core.Domain.PlotArea, PlotAreaDto>(GetArea(areaId));
        }

        public IEnumerable<Core.Domain.PlotArea> GetAreaBySite(byte siteId)
        {
            return _unitOfWork.PlotAreas.GetBySite(siteId);
        }

        public IEnumerable<PlotAreaDto> GetAreaDtosBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.PlotArea>, IEnumerable<PlotAreaDto>>(GetAreaBySite(siteId));
        }
    }
}