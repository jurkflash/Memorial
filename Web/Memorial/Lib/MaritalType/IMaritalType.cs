using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.MaritalType
{
    public interface IMaritalType
    {
        void SetMaritalType(int id);

        Core.Domain.MaritalType GetMaritalType();

        MaritalTypeDto GetMaritalTypeDto();

        Core.Domain.MaritalType GetMaritalTypeById(int id);

        MaritalTypeDto GetMaritalTypeDtoById(int id);

        IEnumerable<Core.Domain.MaritalType> GetMaritalTypes();

        IEnumerable<MaritalTypeDto> GetMaritalTypeDtos();
    }
}