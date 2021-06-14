using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface INiche
    {
        int Create(NicheDto nicheDto);
        bool Delete(int id);
        int? GetApplicantId();
        int GetAreaId();
        IEnumerable<NicheDto> GetAvailableNicheDtosByAreaId(int id);
        IEnumerable<Core.Domain.Niche> GetAvailableNichesByAreaId(int id);
        IEnumerable<NicheDto> GetNicheDtosByAreaIdAndTypeId(int areaId, int typeId, string filter);
        IEnumerable<Core.Domain.Niche> GetNichesByAreaIdAndTypeId(int areaId, int typeId, string filter);
        string GetDescription();
        float GetLifeTimeMaintenance();
        float GetMaintenance();
        string GetName();
        Core.Domain.Niche GetNiche();
        Core.Domain.Niche GetNiche(int id);
        Core.Domain.Niche GetNicheByAreaIdAndPostions(int areaId, int positionX, int positionY);
        NicheDto GetNicheDto();
        NicheDto GetNicheDto(int id);
        NicheDto GetNicheDtoByAreaIdAndPostions(int areaId, int positionX, int positionY);
        IEnumerable<NicheDto> GetNicheDtosByAreaId(int id);
        IEnumerable<Core.Domain.Niche> GetNichesByAreaId(int id);
        int GetNumberOfPlacement();
        IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId);
        float GetPrice();
        bool HasApplicant();
        bool HasDeceased();
        bool HasFreeOrder();
        void RemoveApplicant();
        void SetApplicant(int applicantId);
        void SetHasDeceased(bool flag);
        void SetNiche(int id);
        bool Update(NicheDto nicheDto);
    }
}