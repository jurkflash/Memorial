using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IItem
    {
        void AutoCreateItem(int plotTypeId, int plotId);
        bool Create(CemeteryItemDto cemeteryItemDto);
        bool Delete(int id);
        string GetDescription();
        int GetId();
        CemeteryItem GetItem();
        CemeteryItem GetItem(int id);
        CemeteryItemDto GetItemDto();
        CemeteryItemDto GetItemDto(int id);
        IEnumerable<CemeteryItemDto> GetItemDtosByPlot(int plotId);
        string GetName();
        float GetPrice();
        string GetSystemCode();
        bool IsOrder();
        void SetItem(int id);
        bool Update(CemeteryItemDto cemeteryItemDto);
    }
}