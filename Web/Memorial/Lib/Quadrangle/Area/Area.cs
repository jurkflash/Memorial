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
        private ICentre _centre;
        private Core.Domain.QuadrangleArea _area;

        public Area(IUnitOfWork unitOfWork, ICentre centre)
        {
            _unitOfWork = unitOfWork;
            _centre = centre;
        }

        public void SetArea(int id)
        {
            _area = _unitOfWork.QuadrangleAreas.GetActive(id);
        }

        private void SetCentre()
        {
            _centre.SetCentre(_unitOfWork.QuadrangleCentres.GetActive(_area.QuadrangleCentreId));
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

        public Core.Domain.QuadrangleCentre GetCentre()
        {
            SetCentre();
            return _centre.GetCentre();
        }

        public IEnumerable<Core.Domain.QuadrangleArea> GetByCentre(int centreId)
        {
            return _unitOfWork.QuadrangleAreas.GetByCentre(centreId);
        }

        public IEnumerable<QuadrangleAreaDto> DtosGetByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleArea>, IEnumerable<QuadrangleAreaDto>>(GetByCentre(centreId));
        }
    }
}