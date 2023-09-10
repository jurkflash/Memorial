namespace Memorial.Lib.Cremation
{
    public interface INumber
    {
        string GetNewAF(int cremationItemId, int year);
        string GetNewIV(int cremationItemId, int year);
        string GetNewRE(int cremationItemId, int year);
    }
}