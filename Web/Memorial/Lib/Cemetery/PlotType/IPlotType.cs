using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IPlotType
    {
        IEnumerable<Core.Domain.PlotType> GetAll();
    }
}