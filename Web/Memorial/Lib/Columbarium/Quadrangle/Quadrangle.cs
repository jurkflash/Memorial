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
        private Core.Domain.Niche _quadrangle;

        public Quadrangle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetQuadrangle(int id)
        {
            _quadrangle = _unitOfWork.Niches.GetActive(id);
        }

        public Core.Domain.Niche GetQuadrangle()
        {
            return _quadrangle;
        }

        public NicheDto GetQuadrangleDto()
        {
            return Mapper.Map<Core.Domain.Niche, NicheDto>(GetQuadrangle());
        }

        public Core.Domain.Niche GetQuadrangle(int id)
        {
            return _unitOfWork.Niches.GetActive(id);
        }

        public NicheDto GetQuadrangleDto(int id)
        {
            return Mapper.Map<Core.Domain.Niche, NicheDto>(GetQuadrangle(id));
        }

        public IEnumerable<Core.Domain.Niche> GetQuadranglesByAreaId(int id)
        {
            return _unitOfWork.Niches.GetByArea(id);
        }

        public IEnumerable<NicheDto> GetQuadrangleDtosByAreaId(int id)
        {
            return Mapper.Map< IEnumerable<Core.Domain.Niche>, IEnumerable<NicheDto>>(GetQuadranglesByAreaId(id));
        }

        public IEnumerable<Core.Domain.Niche> GetAvailableQuadranglesByAreaId(int id)
        {
            return _unitOfWork.Niches.GetAvailableByArea(id);
        }

        public IEnumerable<NicheDto> GetAvailableQuadrangleDtosByAreaId(int id)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Niche>, IEnumerable<NicheDto>>(GetAvailableQuadranglesByAreaId(id));
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
            return _quadrangle.ColumbariumAreaId;
        }

        public int GetNumberOfPlacement()
        {
            return _quadrangle.NicheType.NumberOfPlacement;
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId)
        {
            return _unitOfWork.Niches.GetPositionsByArea(areaId);
        }

        public bool Create(NicheDto quadrangleDto)
        {
            _quadrangle = new Core.Domain.Niche();
            Mapper.Map(quadrangleDto, _quadrangle);

            _quadrangle.CreateDate = DateTime.Now;

            _unitOfWork.Niches.Add(_quadrangle);

            return true;
        }

        public bool Update(Core.Domain.Niche quadrangle)
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