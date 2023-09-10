namespace Memorial.Lib.Miscellaneous
{
    public interface INumber
    {
        string GetNewAF(int miscellaneousItemId, int year);
        string GetNewIV(int miscellaneousItemId, int year);
        string GetNewRE(int miscellaneousItemId, int year);
    }
}