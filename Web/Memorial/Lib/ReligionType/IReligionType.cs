using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.ReligionType
{
    public interface IReligionType
    {
        void SetReligionType(int id);

        Core.Domain.ReligionType GetReligionType();

        ReligionTypeDto GetReligionTypeDto();

        Core.Domain.ReligionType GetReligionTypeById(int id);

        ReligionTypeDto GetReligionTypeDtoById(int id);

        IEnumerable<Core.Domain.ReligionType> GetReligionTypes();

        IEnumerable<ReligionTypeDto> GetReligionTypeDtos();
    }
}