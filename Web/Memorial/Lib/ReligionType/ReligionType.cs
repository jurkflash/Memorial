using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.ReligionType
{
    public class ReligionType : IReligionType
    {
        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.ReligionType _religionType;

        public ReligionType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetReligionType(int id)
        {
            _religionType = _unitOfWork.ReligionTypes.GetActive(id);
        }

        public Core.Domain.ReligionType GetReligionType()
        {
            return _religionType;
        }

        public ReligionTypeDto GetReligionTypeDto()
        {
            return Mapper.Map<Core.Domain.ReligionType, ReligionTypeDto>(GetReligionType());
        }

        public Core.Domain.ReligionType GetReligionTypeById(int id)
        {
            return _unitOfWork.ReligionTypes.GetActive(id);
        }

        public ReligionTypeDto GetReligionTypeDtoById(int id)
        {
            return Mapper.Map<Core.Domain.ReligionType, ReligionTypeDto>(GetReligionTypeById(id));
        }

        public IEnumerable<Core.Domain.ReligionType> GetReligionTypes()
        {
            return _unitOfWork.ReligionTypes.GetAllActive();
        }

        public IEnumerable<ReligionTypeDto> GetReligionTypeDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.ReligionType>, IEnumerable<ReligionTypeDto>>(GetReligionTypes());
        }

    }
}