using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface IConfig
    {
        bool CreateArea(ColumbariumAreaDto columbariumAreaDto);
        bool CreateCentre(ColumbariumCentreDto columbariumCentreDto);
        bool CreateItem(ColumbariumItemDto columbariumItemDto);
        bool CreateNiche(NicheDto nicheDto);
        bool DeleteArea(int id);
        bool DeleteCentre(int id);
        bool DeleteItem(int id);
        bool DeleteNiche(int id);
        IEnumerable<ColumbariumAreaDto> GetAreaDtosByCentre(int centreId);
        ColumbariumItemDto GetItemDto(int id);
        IEnumerable<ColumbariumItemDto> GetItemDtosByCentre(int centreId);
        IEnumerable<ColumbariumNumber> GetNumbers();
        ColumbariumAreaDto GetColumbariumAreaDto(int id);
        ColumbariumCentreDto GetColumbariumCentreDto(int id);
        IEnumerable<ColumbariumCentreDto> GetColumbariumCentreDtos();
        NicheDto GetNicheDto(int id);
        IEnumerable<NicheDto> GetNicheDtosByAreaId(int areaId);
        bool UpdateArea(ColumbariumAreaDto columbariumAreaDto);
        bool UpdateCentre(ColumbariumCentreDto columbariumCentreDto);
        bool UpdateItem(ColumbariumItemDto columbariumItemDto);
        bool UpdateNiche(NicheDto nicheDto);
    }
}