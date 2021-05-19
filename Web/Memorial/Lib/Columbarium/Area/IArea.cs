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

        Core.Domain.QuadrangleArea GetArea();

        QuadrangleAreaDto GetAreaDto();


        Core.Domain.QuadrangleArea GetArea(int areaId);

        QuadrangleAreaDto GetAreaDto(int areaId);

        IEnumerable<Core.Domain.QuadrangleArea> GetAreaByCentre(int centreId);

        IEnumerable<QuadrangleAreaDto> GetAreaDtosByCentre(int centreId);

        bool Create(QuadrangleAreaDto quadrangleAreaDto);

        bool Update(Core.Domain.QuadrangleArea quadrangleArea);

        bool Delete(int id);

    }
}