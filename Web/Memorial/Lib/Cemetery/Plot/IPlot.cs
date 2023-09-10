using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IPlot
    {
        Core.Domain.Plot GetById(int id);
        IEnumerable<Core.Domain.Plot> GetByAreaId(int id, string filter);
        IEnumerable<Core.Domain.PlotType> GetPlotTypesByAreaId(int id);
        IEnumerable<Core.Domain.Plot> GetByAreaIdAndTypeId(int areaId, int typeId, string filter);
        IEnumerable<Core.Domain.Plot> GetAvailableByTypeAndArea(int typeId, int areaId);
        int Add(Core.Domain.Plot plot);
        bool Change(int id, Core.Domain.Plot plot);
        bool Remove(int id);
    }
}