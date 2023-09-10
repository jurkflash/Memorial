namespace Memorial.Lib.Columbarium
{
    public interface IPhoto : ITransaction
    {
        bool Add(Core.Domain.ColumbariumTransaction columbariumTransaction);
        bool Change(string AF, Core.Domain.ColumbariumTransaction columbariumTransaction);
        bool Remove(string AF);
        bool ChangeNiche(int oldNicheId, int newNicheId);
    }
}