using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface INiche
    {
        void SetNiche(int id);

        Core.Domain.Niche GetNiche();

        NicheDto GetNicheDto();

        Core.Domain.Niche GetNiche(int id);

        NicheDto GetNicheDto(int id);

        IEnumerable<Core.Domain.Niche> GetNichesByAreaId(int id);

        IEnumerable<NicheDto> GetNicheDtosByAreaId(int id);

        IEnumerable<Core.Domain.Niche> GetAvailableNichesByAreaId(int id);

        IEnumerable<NicheDto> GetAvailableNicheDtosByAreaId(int id);

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

        bool Create(NicheDto nicheDto);

        bool Update(Core.Domain.Niche niche);

        bool Delete(int id);

    }
}