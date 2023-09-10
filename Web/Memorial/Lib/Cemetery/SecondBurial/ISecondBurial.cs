namespace Memorial.Lib.Cemetery
{
    public interface ISecondBurial : ITransaction
    {
        bool Add(Core.Domain.CemeteryTransaction cemeteryTransaction);
        bool Change(string AF, Core.Domain.CemeteryTransaction cemeteryTransaction);
        bool Remove(string AF);
    }
}