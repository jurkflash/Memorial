namespace Memorial.Lib.Space
{
    public interface INumber
    {
        string GetNewAF(int spaceItemId, int year);
        string GetNewIV(int spaceItemId, int year);
        string GetNewRE(int spaceItemId, int year);
    }
}