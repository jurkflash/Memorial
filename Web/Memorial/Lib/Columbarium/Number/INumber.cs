
namespace Memorial.Lib.Columbarium
{
    public interface INumber
    {
        string GetNewAF(int columbariumItemId, int year);
        string GetNewIV(int columbariumItemId, int year);
        string GetNewRE(int columbariumItemId, int year);
    }
}