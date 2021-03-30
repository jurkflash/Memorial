using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface IPlotNumberRepository : IRepository<PlotNumber>
    {
        string GetNewAF(int PlotItemId, int year);

        string GetNewIV(int PlotItemId, int year);

        string GetNewRE(int PlotItemId, int year);
    }
}
