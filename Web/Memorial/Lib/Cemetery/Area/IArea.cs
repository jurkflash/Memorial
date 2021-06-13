using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IArea
    {
        int Create(CemeteryAreaDto cemeteryAreaDto);
        bool Delete(int id);
        CemeteryArea GetArea();
        CemeteryArea GetArea(int areaId);
        IEnumerable<CemeteryArea> GetAreaBySite(int siteId);
        CemeteryAreaDto GetAreaDto();
        CemeteryAreaDto GetAreaDto(int areaId);
        IEnumerable<CemeteryAreaDto> GetAreaDtos();
        IEnumerable<CemeteryAreaDto> GetAreaDtosBySite(int siteId);
        string GetDescription();
        int GetId();
        string GetName();
        int GetSiteId();
        void SetArea(int id);
        bool Update(CemeteryAreaDto cemeteryAreaDto);
    }
}