using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleNumberRepository : IRepository<QuadrangleNumber>
    {
        string GetNewAF(int QuadrangleItemId, int year);

        string GetNewIV(int QuadrangleItemId, int year);

        string GetNewRE(int QuadrangleItemId, int year);
    }
}
