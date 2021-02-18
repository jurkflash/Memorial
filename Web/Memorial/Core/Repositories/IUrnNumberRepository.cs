using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface IUrnNumberRepository : IRepository<UrnNumber>
    {
        string GetNewAF(int UrnItemId, int year);

        string GetNewIV(int UrnItemId, int year);

        string GetNewRE(int UrnItemId, int year);
    }
}
