using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
{
    public interface IAncestralTablet
    {
        void SetAncestralTablet(int id);

        Core.Domain.AncestralTablet GetAncestralTablet();

        AncestralTabletDto GetAncestralTabletDto();

        Core.Domain.AncestralTablet GetAncestralTablet(int id);

        AncestralTabletDto GetAncestralTabletDto(int id);

        IEnumerable<Core.Domain.AncestralTablet> GetAncestralTabletsByAreaId(int id);

        IEnumerable<AncestralTabletDto> GetAncestralTabletDtosByAreaId(int id);

        IEnumerable<Core.Domain.AncestralTablet> GetAvailableAncestralTabletsByAreaId(int id);

        IEnumerable<AncestralTabletDto> GetAvailableAncestralTabletDtosByAreaId(int id);

        Core.Domain.AncestralTablet GetAncestralTabletByAreaIdAndPostions(int areaId, int positionX, int positionY);

        AncestralTabletDto GetAncestralTabletDtoByAreaIdAndPostions(int areaId, int positionX, int positionY);

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

        int Create(AncestralTabletDto ancestralTabletDto);

        bool Update(AncestralTabletDto ancestralTabletDto);

        bool Delete(int id);
    }
}