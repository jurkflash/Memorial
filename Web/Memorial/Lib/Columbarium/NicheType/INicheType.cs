using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface INicheType
    {
        int GetId();
        string GetName();
        Core.Domain.NicheType GetNicheType();
        Core.Domain.NicheType GetNicheType(int nicheTypeId);
        NicheTypeDto GetNicheTypeDto();
        NicheTypeDto GetNicheTypeDto(int nicheTypeId);
        IEnumerable<NicheTypeDto> GetNicheTypeDtos();
        IEnumerable<Core.Domain.NicheType> GetNicheTypes();
        byte GetNumberOfPlacement();
        void SetNicheType(int id);
    }
}