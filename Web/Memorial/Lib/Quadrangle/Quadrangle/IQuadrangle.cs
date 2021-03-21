using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface IQuadrangle
    {
        void SetQuadrangle(int id);

        Core.Domain.Quadrangle GetQuadrangle();

        QuadrangleDto GetQuadrangleDto();

        Core.Domain.Quadrangle GetQuadrangle(int id);

        QuadrangleDto GetQuadrangleDto(int id);

        IEnumerable<Core.Domain.Quadrangle> GetQuadranglesByAreaId(int id);

        IEnumerable<QuadrangleDto> GetQuadrangleDtosByAreaId(int id);

        IEnumerable<Core.Domain.Quadrangle> GetAvailableQuadranglesByAreaId(int id);

        IEnumerable<QuadrangleDto> GetAvailableQuadrangleDtosByAreaId(int id);

        string GetName();

        string GetDescription();

        float GetPrice();

        float GetMaintenance();

        float GetLifeTimeMaintenance();

        bool HasDeceased();

        void SetHasDeceased(bool flag);

        bool HasApplicant();

        int? GetApplicantId();

        void SetApplicant(int applicantId);

        void RemoveApplicant();

        int GetAreaId();

        int GetNumberOfPlacement();

        IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId);

    }
}