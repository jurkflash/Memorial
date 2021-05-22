namespace Memorial.Lib.AncestralTablet
{
    public interface IAncestralTabletDeceased
    {
        bool Add1(int id, int deceasedId);
        bool Remove(int id, int deceasedId);
    }
}