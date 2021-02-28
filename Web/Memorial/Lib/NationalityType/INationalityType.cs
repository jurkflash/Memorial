using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.NationalityType
{
    public interface INationalityType
    {
        void SetNationalityType(int id);

        Core.Domain.NationalityType GetNationalityType();

        NationalityTypeDto GetNationalityTypeDto();

        Core.Domain.NationalityType GetNationalityTypeById(int id);

        NationalityTypeDto GetNationalityTypeDtoById(int id);

        IEnumerable<Core.Domain.NationalityType> GetNationalityTypes();

        IEnumerable<NationalityTypeDto> GetNationalityTypeDtos();
    }
}