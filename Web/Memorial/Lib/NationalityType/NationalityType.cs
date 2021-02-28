using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.NationalityType
{
    public class NationalityType : INationalityType
    {
        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.NationalityType _nationalityType;

        public NationalityType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetNationalityType(int id)
        {
            _nationalityType = _unitOfWork.NationalityTypes.GetActive(id);
        }

        public Core.Domain.NationalityType GetNationalityType()
        {
            return _nationalityType;
        }

        public NationalityTypeDto GetNationalityTypeDto()
        {
            return Mapper.Map<Core.Domain.NationalityType, NationalityTypeDto>(GetNationalityType());
        }

        public Core.Domain.NationalityType GetNationalityTypeById(int id)
        {
            return _unitOfWork.NationalityTypes.GetActive(id);
        }

        public NationalityTypeDto GetNationalityTypeDtoById(int id)
        {
            return Mapper.Map<Core.Domain.NationalityType, NationalityTypeDto>(GetNationalityTypeById(id));
        }

        public IEnumerable<Core.Domain.NationalityType> GetNationalityTypes()
        {
            return _unitOfWork.NationalityTypes.GetAllActive();
        }

        public IEnumerable<NationalityTypeDto> GetNationalityTypeDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.NationalityType>, IEnumerable<NationalityTypeDto>>(GetNationalityTypes());
        }

    }
}