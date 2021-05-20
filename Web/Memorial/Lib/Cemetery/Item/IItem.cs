using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IItem
    {
        void AutoCreateItem(int plotTypeId, int plotId);
        bool Create(PlotItemDto plotItemDto);
        bool Delete(int id);
        string GetDescription();
        int GetId();
        PlotItem GetItem();
        PlotItem GetItem(int id);
        PlotItemDto GetItemDto();
        PlotItemDto GetItemDto(int id);
        IEnumerable<PlotItemDto> GetItemDtosByPlot(int plotId);
        string GetName();
        float GetPrice();
        string GetSystemCode();
        bool IsOrder();
        void SetItem(int id);
        bool Update(PlotItem plotItem);
    }
}