namespace Memorial.Lib.Columbarium
{
    public interface IShift : ITransaction
    {
        bool Add(Core.Domain.ColumbariumTransaction columbariumTransaction);
        bool Change(string AF, Core.Domain.ColumbariumTransaction columbariumTransaction);
        bool Remove(string AF);
    }
}