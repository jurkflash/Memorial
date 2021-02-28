using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.GenderType
{
    public class GenderType : IGenderType
    {
        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.GenderType _genderType;

        public GenderType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetGenderType(int id)
        {
            _genderType = _unitOfWork.GenderTypes.GetActive(id);
        }

        public Core.Domain.GenderType GetGenderType()
        {
            return _genderType;
        }

        public GenderTypeDto GetGenderTypeDto()
        {
            return Mapper.Map<Core.Domain.GenderType, GenderTypeDto>(GetGenderType());
        }

        public Core.Domain.GenderType GetGenderTypeById(int id)
        {
            return _unitOfWork.GenderTypes.GetActive(id);
        }

        public GenderTypeDto GetGenderTypeDtoById(int id)
        {
            return Mapper.Map<Core.Domain.GenderType, GenderTypeDto>(GetGenderTypeById(id));
        }

        public IEnumerable<Core.Domain.GenderType> GetGenderTypes()
        {
            return _unitOfWork.GenderTypes.GetAllActive();
        }

        public IEnumerable<GenderTypeDto> GetGenderTypeDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.GenderType>, IEnumerable<GenderTypeDto>>(GetGenderTypes());
        }

    }
}