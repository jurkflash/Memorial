using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IQuadrangleCentre
    {
        void SetById(int id);

        IEnumerable<Core.Domain.QuadrangleCentre> GetBySite(byte sitId);

        IEnumerable<QuadrangleCentreDto> DtosGetBySite(byte sitId);

    }
}