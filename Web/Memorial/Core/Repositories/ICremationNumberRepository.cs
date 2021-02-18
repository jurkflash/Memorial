using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface ICremationNumberRepository : IRepository<CremationNumber>
    {
        string GetNewAF(int CremationItemId, int year);

        string GetNewIV(int CremationItemId, int year);

        string GetNewRE(int CremationItemId, int year);
    }
}
