using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class NicheType : INicheType
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.NicheType _nicheType;

        public NicheType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetNicheType(int id)
        {
            _nicheType = _unitOfWork.NicheTypes.Get(id);
        }

        public int GetId()
        {
            return _nicheType.Id;
        }

        public string GetName()
        {
            return _nicheType.Name;
        }

        public byte GetNumberOfPlacement()
        {
            return _nicheType.NumberOfPlacement;
        }

        public Core.Domain.NicheType GetNicheType()
        {
            return _nicheType;
        }

        public NicheTypeDto GetNicheTypeDto()
        {
            return Mapper.Map<Core.Domain.NicheType, NicheTypeDto>(_nicheType);
        }

        public Core.Domain.NicheType GetNicheType(int nicheTypeId)
        {
            return _unitOfWork.NicheTypes.Get(nicheTypeId);
        }

        public NicheTypeDto GetNicheTypeDto(int nicheTypeId)
        {
            return Mapper.Map<Core.Domain.NicheType, NicheTypeDto>(GetNicheType(nicheTypeId));
        }

        public IEnumerable<Core.Domain.NicheType> GetNicheTypes()
        {
            return _unitOfWork.NicheTypes.GetAll();
        }

        public IEnumerable<NicheTypeDto> GetNicheTypeDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.NicheType>, IEnumerable<NicheTypeDto>>(GetNicheTypes());
        }

    }
}