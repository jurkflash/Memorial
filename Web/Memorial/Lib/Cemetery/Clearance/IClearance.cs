namespace Memorial.Lib.Cemetery
{
    public interface IClearance : ITransaction
    {
        bool Add(Core.Domain.CemeteryTransaction cemeteryTransaction);
        bool Remove(string AF);
        bool Change(string AF, Core.Domain.CemeteryTransaction cemeteryTransaction);
    }
}