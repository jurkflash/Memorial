using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface IQuadrangle
    {
        void SetQuadrangle(int id);

        Core.Domain.Niche GetQuadrangle();

        NicheDto GetQuadrangleDto();

        Core.Domain.Niche GetQuadrangle(int id);

        NicheDto GetQuadrangleDto(int id);

        IEnumerable<Core.Domain.Niche> GetQuadranglesByAreaId(int id);

        IEnumerable<NicheDto> GetQuadrangleDtosByAreaId(int id);

        IEnumerable<Core.Domain.Niche> GetAvailableQuadranglesByAreaId(int id);

        IEnumerable<NicheDto> GetAvailableQuadrangleDtosByAreaId(int id);

        string GetName();

        string GetDescription();

        float GetPrice();

        float GetMaintenance();

        float GetLifeTimeMaintenance();

        bool HasDeceased();

        void SetHasDeceased(bool flag);

        bool HasFreeOrder();

        bool HasApplicant();

        int? GetApplicantId();

        void SetApplicant(int applicantId);

        void RemoveApplicant();

        int GetAreaId();

        int GetNumberOfPlacement();

        IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId);

        bool Create(NicheDto quadrangleDto);

        bool Update(Core.Domain.Niche quadrangle);

        bool Delete(int id);

    }
}