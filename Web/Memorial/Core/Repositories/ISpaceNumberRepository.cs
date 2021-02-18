using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface ISpaceNumberRepository : IRepository<SpaceNumber>
    {
        string GetNewAF(int SpaceItemId, int year);

        string GetNewIV(int SpaceItemId, int year);

        string GetNewRE(int SpaceItemId, int year);
    }
}
