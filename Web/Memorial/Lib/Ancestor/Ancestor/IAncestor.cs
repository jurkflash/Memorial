using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface IAncestor
    {
        void SetAncestor(int id);

        Core.Domain.Ancestor GetAncestor();

        AncestorDto GetAncestorDto();

        Core.Domain.Ancestor GetAncestor(int id);

        AncestorDto GetAncestorDto(int id);

        IEnumerable<Core.Domain.Ancestor> GetAncestorsByAreaId(int id);

        IEnumerable<AncestorDto> GetAncestorDtosByAreaId(int id);

        IEnumerable<Core.Domain.Ancestor> GetAvailableAncestorsByAreaId(int id);

        IEnumerable<AncestorDto> GetAvailableAncestorDtosByAreaId(int id);

        string GetName();

        float GetPrice();

        float GetMaintenance();

        bool HasDeceased();

        void SetHasDeceased(bool flag);

        bool HasApplicant();

        bool HasFreeOrder();

        int? GetApplicantId();

        void SetApplicant(int applicantId);

        void RemoveApplicant();

        int GetAreaId();

        IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId);

        bool Create(AncestorDto ancestorDto);

        bool Update(Core.Domain.Ancestor ancestor);

        bool Delete(int id);
    }
}