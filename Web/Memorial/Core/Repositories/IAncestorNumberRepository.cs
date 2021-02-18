using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface IAncestorNumberRepository : IRepository<AncestorNumber>
    {
        string GetNewAF(int AncestorItemId, int year);

        string GetNewIV(int AncestorItemId, int year);

        string GetNewRE(int AncestorItemId, int year);
    }
}
