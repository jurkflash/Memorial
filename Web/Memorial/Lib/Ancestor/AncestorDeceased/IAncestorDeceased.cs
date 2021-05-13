namespace Memorial.Lib.Ancestor
{
    public interface IAncestorDeceased
    {
        bool Add1(int id, int deceasedId);
        bool Remove(int id, int deceasedId);
    }
}