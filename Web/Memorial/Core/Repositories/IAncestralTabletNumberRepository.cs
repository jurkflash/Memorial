using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface IAncestralTabletNumberRepository : IRepository<AncestralTabletNumber>
    {
        string GetNewAF(int AncestralTabletItemId, int year);

        string GetNewIV(int AncestralTabletItemId, int year);

        string GetNewRE(int AncestralTabletItemId, int year);
    }
}
