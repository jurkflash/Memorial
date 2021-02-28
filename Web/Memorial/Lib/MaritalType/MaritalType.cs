using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.MaritalType
{
    public class MaritalType : IMaritalType
    {
        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.MaritalType _maritalType;

        public MaritalType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetMaritalType(int id)
        {
            _maritalType = _unitOfWork.MaritalTypes.GetActive(id);
        }

        public Core.Domain.MaritalType GetMaritalType()
        {
            return _maritalType;
        }

        public MaritalTypeDto GetMaritalTypeDto()
        {
            return Mapper.Map<Core.Domain.MaritalType, MaritalTypeDto>(GetMaritalType());
        }

        public Core.Domain.MaritalType GetMaritalTypeById(int id)
        {
            return _unitOfWork.MaritalTypes.GetActive(id);
        }

        public MaritalTypeDto GetMaritalTypeDtoById(int id)
        {
            return Mapper.Map<Core.Domain.MaritalType, MaritalTypeDto>(GetMaritalTypeById(id));
        }

        public IEnumerable<Core.Domain.MaritalType> GetMaritalTypes()
        {
            return _unitOfWork.MaritalTypes.GetAllActive();
        }

        public IEnumerable<MaritalTypeDto> GetMaritalTypeDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.MaritalType>, IEnumerable<MaritalTypeDto>>(GetMaritalTypes());
        }

    }
}