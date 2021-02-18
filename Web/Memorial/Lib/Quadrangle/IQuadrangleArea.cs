using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IQuadrangleArea
    {
        void SetById(int id);

        IEnumerable<Core.Domain.QuadrangleArea> GetByCentre(int centreId);

        IEnumerable<QuadrangleAreaDto> DtosGetByCentre(int centreId);

    }
}