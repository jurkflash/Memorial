using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IItem
    {
        void AutoAddItem(int plotTypeId, int plotId);
        CemeteryItem GetById(int id);
        IEnumerable<CemeteryItemDto> GetItemDtosByPlot(int plotId);
        bool Change(int id, Core.Domain.CemeteryItem cemeteryItem);
        float GetPrice(Core.Domain.CemeteryItem cemeteryItem);
        bool IsOrder(Core.Domain.CemeteryItem cemeteryItem);
    }
}