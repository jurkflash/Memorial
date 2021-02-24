using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Quadrangle
{
    public class Quadrangle : IQuadrangle
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICentre _centre;
        private IArea _area;
        private IItem _item;
        private Core.Domain.Quadrangle _quadrangle;

        public Quadrangle(IUnitOfWork unitOfWork, ICentre centre, IArea area, IItem item)
        {
            _unitOfWork = unitOfWork;
            _centre = centre;
            _area = area;
            _item = item;
        }

        public void SetQuadrangle(int id)
        {
            _quadrangle = _unitOfWork.Quadrangles.GetActive(id);
        }

        private void SetArea()
        {
            _area.SetArea(_quadrangle.QuadrangleAreaId);
        }

        private void SetCentre()
        {
            SetArea();
            _centre.SetCentre(_area.GetCentreId());
        }

        public string GetName()
        {
            return _quadrangle.Name;
        }

        public string GetDescription()
        {
            return _quadrangle.Description;
        }

        public float GetPrice()
        {
            return _quadrangle.Price;
        }

        public float GetMaintenance()
        {
            return _quadrangle.Maintenance;
        }

        public float GetLifeTimeMaintenance()
        {
            return _quadrangle.LifeTimeMaintenance;
        }

        public bool HasDeceased()
        {
            return _quadrangle.hasDeceased;
        }

        public bool HasApplicant()
        {
            return _quadrangle.ApplicantId == null ? false : true;
        }

        public int GetAreaId()
        {
            return _quadrangle.QuadrangleAreaId;
        }

        public int GetCentreId()
        {
            SetCentre();
            return _centre.GetID();
        }

        public int GetNumberOfPlacement()
        {
            return _quadrangle.QuadrangleType.NumberOfPlacement;
        }

        public void SetApplicant(int applicantId)
        {
            _quadrangle.ApplicantId = applicantId;
        }

        public void RemoveApplicant()
        {
            _quadrangle.Applicant = null;
            _quadrangle.ApplicantId = null;
        }

        public void SetHasDeceased(bool flag)
        {
            _quadrangle.hasDeceased = flag;
        }

        public Core.Domain.Quadrangle GetQuadrangle()
        {
            return _quadrangle;
        }

        public Core.Domain.QuadrangleArea GetArea()
        {
            SetArea();
            return _area.GetArea();
        }

        public Core.Domain.QuadrangleCentre GetCentre()
        {
            SetCentre();
            return _centre.GetCentre();
        }

        public IEnumerable<Core.Domain.QuadrangleItem> GetItems()
        {
            SetCentre();
            return _item.GetByCentre(_centre.GetID());
        }

        public QuadrangleDto DtoGetQuadrangle()
        {
            return Mapper.Map<Core.Domain.Quadrangle, QuadrangleDto>(GetQuadrangle());
        }   

        public IEnumerable<QuadrangleDto> DtosGetByArea(int areaId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Quadrangle>, IEnumerable<QuadrangleDto>>(_unitOfWork.Quadrangles.GetByArea(areaId));
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int areaId)
        {
            return _unitOfWork.Quadrangles.GetPositionsByArea(areaId);
        }

    }
}