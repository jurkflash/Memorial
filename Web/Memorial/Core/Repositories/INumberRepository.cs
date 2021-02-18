using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface INumberRepository
    {
        int GetCremationNewAF(int itemId, int year);

        int GetCremationNewIV(int itemId, int year);

        int GetCremationNewRE(int itemId, int year);

        int GetMiscellaneousNewAF(int itemId, int year);

        int GetMiscellaneousNewIV(int itemId, int year);

        int GetMiscellaneousNewRE(int itemId, int year);

        int GetSpaceNewAF(int itemId, int year);

        int GetSpaceNewIV(int itemId, int year);

        int GetSpaceNewRE(int itemId, int year);

        int GetUrnNewAF(int itemId, int year);

        int GetUrnNewIV(int itemId, int year);

        int GetUrnNewRE(int itemId, int year);

        int GetQuadrangleNewAF(int itemId, int year);

        int GetQuadrangleNewIV(int itemId, int year);

        int GetQuadrangleNewRE(int itemId, int year);

        int GetAncestorNewAF(int itemId, int year);

        int GetAncestorNewIV(int itemId, int year);

        int GetAncestorNewRE(int itemId, int year);
    }
}
