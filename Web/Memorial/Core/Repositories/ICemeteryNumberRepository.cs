using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface ICemeteryNumberRepository : IRepository<CemeteryNumber>
    {
        string GetNewAF(int CemeteryItemId, int year);

        string GetNewIV(int CemeteryItemId, int year);

        string GetNewRE(int CemeteryItemId, int year);
    }
}
