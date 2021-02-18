using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface IMiscellaneousNumberRepository : IRepository<MiscellaneousNumber>
    {
        string GetNewAF(int MiscellaneousItemId, int year);

        string GetNewIV(int MiscellaneousItemId, int year);

        string GetNewRE(int MiscellaneousItemId, int year);
    }
}
