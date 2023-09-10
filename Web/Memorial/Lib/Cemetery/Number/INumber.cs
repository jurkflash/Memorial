namespace Memorial.Lib.Cemetery
{
    public interface INumber
    {
        string GetNewAF(int cemeteryItemId, int year);
        string GetNewIV(int cemeteryItemId, int year);
        string GetNewRE(int cemeteryItemId, int year);
    }
}