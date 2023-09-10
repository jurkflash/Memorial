namespace Memorial.Lib.Cremation
{
    public interface IOrder : ITransaction
    {
        bool Add(Core.Domain.CremationTransaction cremationTransaction);
        bool Change(string AF, Core.Domain.CremationTransaction cremationTransaction);
        bool Remove(string AF);
    }
}