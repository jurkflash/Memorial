using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface IArea
    {
        void SetArea(int id);

        int GetId();

        string GetName();

        string GetDescription();

        int GetCentreId();

        Core.Domain.ColumbariumArea GetArea();

        ColumbariumAreaDto GetAreaDto();


        Core.Domain.ColumbariumArea GetArea(int areaId);

        ColumbariumAreaDto GetAreaDto(int areaId);

        IEnumerable<Core.Domain.ColumbariumArea> GetAreaByCentre(int centreId);

        IEnumerable<ColumbariumAreaDto> GetAreaDtosByCentre(int centreId);

        bool Create(ColumbariumAreaDto columbariumAreaDto);

        bool Update(Core.Domain.ColumbariumArea columbariumArea);

        bool Delete(int id);

    }
}