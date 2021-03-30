using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public interface IItem
    {
        void SetItem(int id);

        Core.Domain.PlotItem GetItem();

        PlotItemDto GetItemDto();

        Core.Domain.PlotItem GetItem(int id);

        PlotItemDto GetItemDto(int id);

        int GetId();

        string GetName();

        string GetDescription();

        float GetPrice();

        string GetSystemCode();

        bool IsOrder();

        IEnumerable<Core.Domain.PlotItem> GetItemByPlot(int plotId);

        IEnumerable<PlotItemDto> GetItemDtosByPlot(int plotId);
    }
}