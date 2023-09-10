namespace Memorial.Lib.Cemetery
{
    public interface IOrder : ITransaction
    {
        bool Add(Core.Domain.CemeteryTransaction cemeteryTransaction);
        bool Change(string AF, Core.Domain.CemeteryTransaction cemeteryTransaction);
        bool Remove(string AF);
    }
}