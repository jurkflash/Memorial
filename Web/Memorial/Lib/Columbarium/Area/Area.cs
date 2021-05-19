using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Area : IArea
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.ColumbariumArea _area;

        public Area(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetArea(int id)
        {
            _area = _unitOfWork.ColumbariumAreas.GetActive(id);
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
            return _area.ColumbariumCentreId;
        }

        public Core.Domain.ColumbariumArea GetArea()
        {
            return _area;
        }

        public ColumbariumAreaDto GetAreaDto()
        {
            return Mapper.Map<Core.Domain.ColumbariumArea, ColumbariumAreaDto>(_area);
        }

        public Core.Domain.ColumbariumArea GetArea(int areaId)
        {
            return _unitOfWork.ColumbariumAreas.GetActive(areaId);
        }

        public ColumbariumAreaDto GetAreaDto(int areaId)
        {
            return Mapper.Map<Core.Domain.ColumbariumArea, ColumbariumAreaDto>(GetArea(areaId));
        }

        public IEnumerable<Core.Domain.ColumbariumArea> GetAreaByCentre(int centreId)
        {
            return _unitOfWork.ColumbariumAreas.GetByCentre(centreId);
        }

        public IEnumerable<ColumbariumAreaDto> GetAreaDtosByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumArea>, IEnumerable<ColumbariumAreaDto>>(GetAreaByCentre(centreId));
        }

        public bool Create(ColumbariumAreaDto columbariumAreaDto)
        {
            _area = new Core.Domain.ColumbariumArea();
            Mapper.Map(columbariumAreaDto, _area);

            _area.CreateDate = DateTime.Now;

            _unitOfWork.ColumbariumAreas.Add(_area);

            return true;
        }

        public bool Update(Core.Domain.ColumbariumArea columbariumArea)
        {
            columbariumArea.ModifyDate = DateTime.Now;

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