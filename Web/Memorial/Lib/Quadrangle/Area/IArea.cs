using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface IArea
    {
        void SetArea(int id);

        int GetId();

        string GetName();

        string GetDescription();

        int GetCentreId();

        Core.Domain.QuadrangleArea GetArea();

        Core.Domain.QuadrangleCentre GetCentre();

        IEnumerable<Core.Domain.QuadrangleArea> GetByCentre(int centreId);

        IEnumerable<QuadrangleAreaDto> DtosGetByCentre(int centreId);

    }
}