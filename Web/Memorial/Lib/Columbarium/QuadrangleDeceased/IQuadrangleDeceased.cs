namespace Memorial.Lib.Columbarium
{
    public interface IQuadrangleDeceased
    {
        bool Add1(int id, int deceasedId);
        bool Add2(int id, int deceasedId);
        bool Remove(int id, int deceasedId);
    }
}