using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IQuadrangle
    {
        void SetQuadrangle(int id);

        string GetName();

        string GetDescription();

        float GetPrice();

        float GetMaintenance();

        float GetLifeTimeMaintenance();

        bool HasDeceased();

        bool HasApplicant();

        int GetAreaId();

        int GetNumberOfPlacement();

        void SetApplicant(int applicantId);

        void RemoveApplicant();

        void SetHasDeceased(bool flag);

        Core.Domain.Quadrangle GetQuadrangle();

        Core.Domain.QuadrangleArea GetArea();

        Core.Domain.QuadrangleCentre GetCentre();

        IEnumerable<Core.Domain.QuadrangleItem> GetItems();

        QuadrangleDto DtoGetQuadrangle();

        IEnumerable<QuadrangleDto> DtosGetByArea(int areaId);

        IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int areaId);

    }
}