using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface IPlotNumberRepository : IRepository<PlotNumber>
    {
        string GetNewAF(int CemeteryItemId, int year);

        string GetNewIV(int CemeteryItemId, int year);

        string GetNewRE(int CemeteryItemId, int year);
    }
}
