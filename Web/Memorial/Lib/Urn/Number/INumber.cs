namespace Memorial.Lib.Urn
{
    public interface INumber
    {
        string GetNewAF(int urnItemId, int year);
        string GetNewIV(int urnItemId, int year);
        string GetNewRE(int urnItemId, int year);
    }
}