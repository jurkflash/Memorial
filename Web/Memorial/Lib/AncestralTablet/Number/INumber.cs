namespace Memorial.Lib.AncestralTablet
{
    public interface INumber
    {
        string GetNewAF(int ancestralTabletItemId, int year);
        string GetNewIV(int ancestralTabletItemId, int year);
        string GetNewRE(int ancestralTabletItemId, int year);
    }
}