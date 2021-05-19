using System;
using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Niche : INiche
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Niche _niche;

        public Niche(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetNiche(int id)
        {
            _niche = _unitOfWork.Niches.GetActive(id);
        }

        public Core.Domain.Niche GetNiche()
        {
            return _niche;
        }

        public NicheDto GetNicheDto()
        {
            return Mapper.Map<Core.Domain.Niche, NicheDto>(GetNiche());
        }

        public Core.Domain.Niche GetNiche(int id)
        {
            return _unitOfWork.Niches.GetActive(id);
        }

        public NicheDto GetNicheDto(int id)
        {
            return Mapper.Map<Core.Domain.Niche, NicheDto>(GetNiche(id));
        }

        public IEnumerable<Core.Domain.Niche> GetNichesByAreaId(int id)
        {
            return _unitOfWork.Niches.GetByArea(id);
        }

        public IEnumerable<NicheDto> GetNicheDtosByAreaId(int id)
        {
            return Mapper.Map< IEnumerable<Core.Domain.Niche>, IEnumerable<NicheDto>>(GetNichesByAreaId(id));
        }

        public IEnumerable<Core.Domain.Niche> GetAvailableNichesByAreaId(int id)
        {
            return _unitOfWork.Niches.GetAvailableByArea(id);
        }

        public IEnumerable<NicheDto> GetAvailableNicheDtosByAreaId(int id)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Niche>, IEnumerable<NicheDto>>(GetAvailableNichesByAreaId(id));
        }

        public string GetName()
        {
            return _niche.Name;
        }

        public string GetDescription()
        {
            return _niche.Description;
        }

        public float GetPrice()
        {
            return _niche.Price;
        }

        public float GetMaintenance()
        {
            return _niche.Maintenance;
        }

        public float GetLifeTimeMaintenance()
        {
            return _niche.LifeTimeMaintenance;
        }

        public bool HasDeceased()
        {
            return _niche.hasDeceased;
        }

        public void SetHasDeceased(bool flag)
        {
            _niche.hasDeceased = flag;
        }

        public bool HasApplicant()
        {
            return _niche.ApplicantId == null ? false : true;
        }

        public bool HasFreeOrder()
        {
            return _niche.hasFreeOrder;
        }

        public int? GetApplicantId()
        {
            return _niche.ApplicantId;
        }

        public void SetApplicant(int applicantId)
        {
            _niche.ApplicantId = applicantId;
        }

        public void RemoveApplicant()
        {
            _niche.Applicant = null;
            _niche.ApplicantId = null;
        }

        public int GetAreaId()
        {
            return _niche.ColumbariumAreaId;
        }

        public int GetNumberOfPlacement()
        {
            return _niche.NicheType.NumberOfPlacement;
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId)
        {
            return _unitOfWork.Niches.GetPositionsByArea(areaId);
        }

        public bool Create(NicheDto nicheDto)
        {
            _niche = new Core.Domain.Niche();
            Mapper.Map(nicheDto, _niche);

            _niche.CreateDate = DateTime.Now;

            _unitOfWork.Niches.Add(_niche);

            return true;
        }

        public bool Update(Core.Domain.Niche niche)
        {
            niche.ModifyDate = DateTime.Now;

            return true;
        }

        public bool Delete(int id)
        {
            SetNiche(id);

            _niche.DeleteDate = DateTime.Now;

            return true;
        }
    }
}