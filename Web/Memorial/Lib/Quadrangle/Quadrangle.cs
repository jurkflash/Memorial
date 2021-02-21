using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class Quadrangle : IQuadrangle
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Quadrangle _quadrangle;

        public Quadrangle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<QuadrangleDto> DtosGetByArea(int areaId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Quadrangle>, IEnumerable<QuadrangleDto>>(_unitOfWork.Quadrangles.GetByArea(areaId));
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int areaId)
        {
            return _unitOfWork.Quadrangles.GetPositionsByArea(areaId);
        }

        public void SetById(int id)
        {
            _quadrangle = _unitOfWork.Quadrangles.GetActive(id);
        }

        public Core.Domain.Quadrangle GetQuadrangle()
        {
            return _quadrangle;
        }

        public QuadrangleDto DtoGetQuadrangle()
        {
            return Mapper.Map<Core.Domain.Quadrangle, QuadrangleDto>(_quadrangle);
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

        public QuadrangleDto DtosGetById(int quadrangleId)
        {
            SetById(quadrangleId);
            return Mapper.Map<Core.Domain.Quadrangle, QuadrangleDto>(_quadrangle);
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
    }
}