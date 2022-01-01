using System;
using System.Collections.Generic;
using System.Linq;
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
            return Mapper.Map<IEnumerable<Core.Domain.Niche>, IEnumerable<NicheDto>>(GetNichesByAreaId(id));
        }

        public IEnumerable<Core.Domain.Niche> GetAvailableNichesByAreaId(int id)
        {
            return _unitOfWork.Niches.GetAvailableByArea(id);
        }

        public IEnumerable<Core.Domain.Niche> GetNichesByAreaIdAndTypeId(int areaId, int typeId, string filter)
        {
            return _unitOfWork.Niches.GetByTypeAndArea(areaId, typeId, filter);
        }

        public IEnumerable<NicheDto> GetNicheDtosByAreaIdAndTypeId(int areaId, int typeId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Niche>, IEnumerable<NicheDto>>(GetNichesByAreaIdAndTypeId(areaId, typeId, filter));
        }

        public IEnumerable<NicheDto> GetAvailableNicheDtosByAreaId(int id)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Niche>, IEnumerable<NicheDto>>(GetAvailableNichesByAreaId(id));
        }

        public Core.Domain.Niche GetNicheByAreaIdAndPostions(int areaId, int positionX, int positionY)
        {
            return _unitOfWork.Niches.GetByAreaAndPositions(areaId, positionX, positionY);
        }

        public NicheDto GetNicheDtoByAreaIdAndPostions(int areaId, int positionX, int positionY)
        {
            return Mapper.Map<Core.Domain.Niche, NicheDto>(GetNicheByAreaIdAndPostions(areaId, positionX, positionY));
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

        public int Create(NicheDto nicheDto)
        {
            _niche = new Core.Domain.Niche();
            Mapper.Map(nicheDto, _niche);

            _niche.CreatedDate = DateTime.Now;

            _unitOfWork.Niches.Add(_niche);

            _unitOfWork.Complete();

            return _niche.Id;
        }

        public bool Update(NicheDto nicheDto)
        {
            var nicheInDB = GetNiche(nicheDto.Id);

            if ((nicheInDB.NicheTypeId != nicheDto.NicheTypeDtoId
                || nicheInDB.ColumbariumAreaId != nicheDto.ColumbariumAreaDtoId)
                && _unitOfWork.ColumbariumTransactions.Find(qt => (qt.NicheId == nicheDto.Id || qt.ShiftedNicheId == nicheDto.Id) && qt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(nicheDto, nicheInDB);

            nicheInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => (qt.NicheId == id || qt.ShiftedNicheId == id) && qt.DeleteDate == null).Any())
            {
                return false;
            }

            SetNiche(id);

            _niche.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}