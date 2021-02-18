using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IQuadrangle
    {
        QuadrangleDto DtosGetById(int quadrangleId);

        IEnumerable<QuadrangleDto> DtosGetByArea(int areaId);

        IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int areaId);

        void SetById(int id);

        Core.Domain.Quadrangle GetQuadrangle();

        string GetName();

        string GetDescription();

        float GetPrice();

        float GetMaintenance();

        float GetLifeTimeMaintenance();

        void SetHasDeceased();
    }
}