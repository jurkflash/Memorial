using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumNumberRepository : IRepository<ColumbariumNumber>
    {
        string GetNewAF(int ColumbariumItemId, int year);

        string GetNewIV(int ColumbariumItemId, int year);

        string GetNewRE(int ColumbariumItemId, int year);
    }
}
