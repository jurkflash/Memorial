using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.GenderType
{
    public interface IGenderType
    {
        void SetGenderType(int id);

        Core.Domain.GenderType GetGenderType();

        GenderTypeDto GetGenderTypeDto();

        Core.Domain.GenderType GetGenderTypeById(int id);

        GenderTypeDto GetGenderTypeDtoById(int id);

        IEnumerable<Core.Domain.GenderType> GetGenderTypes();

        IEnumerable<GenderTypeDto> GetGenderTypeDtos();
    }
}