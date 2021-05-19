using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Quadrangle : IQuadrangle
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Quadrangle _quadrangle;

        public Quadrangle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetQuadrangle(int id)
        {
            _quadrangle = _unitOfWork.Quadrangles.GetActive(id);
        }

        public Core.Domain.Quadrangle GetQuadrangle()
        {
            return _quadrangle;
        }

        public QuadrangleDto GetQuadrangleDto()
        {
            return Mapper.Map<Core.Domain.Quadrangle, QuadrangleDto>(GetQuadrangle());
        }

        public Core.Domain.Quadrangle GetQuadrangle(int id)
        {
            return _unitOfWork.Quadrangles.GetActive(id);
        }

        public QuadrangleDto GetQuadrangleDto(int id)
        {
            return Mapper.Map<Core.Domain.Quadrangle, QuadrangleDto>(GetQuadrangle(id));
        }

        public IEnumerable<Core.Domain.Quadrangle> GetQuadranglesByAreaId(int id)
        {
            return _unitOfWork.Quadrangles.GetByArea(id);
        }

        public IEnumerable<QuadrangleDto> GetQuadrangleDtosByAreaId(int id)
        {
            return Mapper.Map< IEnumerable<Core.Domain.Quadrangle>, IEnumerable<QuadrangleDto>>(GetQuadranglesByAreaId(id));
        }

        public IEnumerable<Core.Domain.Quadrangle> GetAvailableQuadranglesByAreaId(int id)
        {
            return _unitOfWork.Quadrangles.GetAvailableByArea(id);
        }

        public IEnumerable<QuadrangleDto> GetAvailableQuadrangleDtosByAreaId(int id)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Quadrangle>, IEnumerable<QuadrangleDto>>(GetAvailableQuadranglesByAreaId(id));
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

        public void SetHasDeceased(bool flag)
        {
            _quadrangle.hasDeceased = flag;
        }

        public bool HasApplicant()
        {
            return _quadrangle.ApplicantId == null ? false : true;
        }

        public bool HasFreeOrder()
        {
            return _quadrangle.hasFreeOrder;
        }

        public int? GetApplicantId()
        {
            return _quadrangle.ApplicantId;
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

        public int GetAreaId()
        {
            return _quadrangle.QuadrangleAreaId;
        }

        public int GetNumberOfPlacement()
        {
            return _quadrangle.QuadrangleType.NumberOfPlacement;
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId)
        {
            return _unitOfWork.Quadrangles.GetPositionsByArea(areaId);
        }

        public bool Create(QuadrangleDto quadrangleDto)
        {
            _quadrangle = new Core.Domain.Quadrangle();
            Mapper.Map(quadrangleDto, _quadrangle);

            _quadrangle.CreateDate = DateTime.Now;

            _unitOfWork.Quadrangles.Add(_quadrangle);

            return true;
        }

        public bool Update(Core.Domain.Quadrangle quadrangle)
        {
            quadrangle.ModifyDate = DateTime.Now;

            return true;
        }

        public bool Delete(int id)
        {
            SetQuadrangle(id);

            _quadrangle.DeleteDate = DateTime.Now;

            return true;
        }
    }
}