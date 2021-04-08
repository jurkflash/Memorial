using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Quadrangle
{
    public class Area : IArea
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.QuadrangleArea _area;

        public Area(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetArea(int id)
        {
            _area = _unitOfWork.QuadrangleAreas.GetActive(id);
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

        public int GetCentreId()
        {
            return _area.QuadrangleCentreId;
        }

        public Core.Domain.QuadrangleArea GetArea()
        {
            return _area;
        }

        public QuadrangleAreaDto GetAreaDto()
        {
            return Mapper.Map<Core.Domain.QuadrangleArea, QuadrangleAreaDto>(_area);
        }

        public Core.Domain.QuadrangleArea GetArea(int areaId)
        {
            return _unitOfWork.QuadrangleAreas.GetActive(areaId);
        }

        public QuadrangleAreaDto GetAreaDto(int areaId)
        {
            return Mapper.Map<Core.Domain.QuadrangleArea, QuadrangleAreaDto>(GetArea(areaId));
        }

        public IEnumerable<Core.Domain.QuadrangleArea> GetAreaByCentre(int centreId)
        {
            return _unitOfWork.QuadrangleAreas.GetByCentre(centreId);
        }

        public IEnumerable<QuadrangleAreaDto> GetAreaDtosByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleArea>, IEnumerable<QuadrangleAreaDto>>(GetAreaByCentre(centreId));
        }

        public bool Create(QuadrangleAreaDto quadrangleAreaDto)
        {
            _area = new Core.Domain.QuadrangleArea();
            Mapper.Map(quadrangleAreaDto, _area);

            _area.CreateDate = DateTime.Now;

            _unitOfWork.QuadrangleAreas.Add(_area);

            return true;
        }

        public bool Update(Core.Domain.QuadrangleArea quadrangleArea)
        {
            quadrangleArea.ModifyDate = DateTime.Now;

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