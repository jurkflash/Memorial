using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface INumberRepository
    {
        int GetCremationNewAF(string itemCode, int year);

        int GetCremationNewIV(string itemCode, int year);

        int GetCremationNewRE(string itemCode, int year);

        int GetMiscellaneousNewAF(string itemCode, int year);

        int GetMiscellaneousNewIV(string itemCode, int year);

        int GetMiscellaneousNewRE(string itemCode, int year);

        int GetSpaceNewAF(string itemCode, int year);

        int GetSpaceNewIV(string itemCode, int year);

        int GetSpaceNewRE(string itemCode, int year);

        int GetUrnNewAF(string itemCode, int year);

        int GetUrnNewIV(string itemCode, int year);

        int GetUrnNewRE(string itemCode, int year);

        int GetColumbariumNewAF(string itemCode, int year);

        int GetColumbariumNewIV(string itemCode, int year);

        int GetColumbariumNewRE(string itemCode, int year);

        int GetAncestorNewAF(string itemCode, int year);

        int GetAncestorNewIV(string itemCode, int year);

        int GetAncestorNewRE(string itemCode, int year);

        int GetPlotNewAF(string itemCode, int year);

        int GetPlotNewIV(string itemCode, int year);

        int GetPlotNewRE(string itemCode, int year);
    }
}
