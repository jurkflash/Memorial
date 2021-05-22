using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface IAncestorNumberRepository : IRepository<AncestorNumber>
    {
        string GetNewAF(int AncestralTabletItemId, int year);

        string GetNewIV(int AncestralTabletItemId, int year);

        string GetNewRE(int AncestralTabletItemId, int year);
    }
}
