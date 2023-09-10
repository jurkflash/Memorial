namespace Memorial.Lib.Urn
{
    public interface IPurchase : ITransaction
    {
        bool Add(Core.Domain.UrnTransaction urnTransaction);
        bool Change(string AF, Core.Domain.UrnTransaction urnTransaction);
        bool Remove(string AF);
    }
}