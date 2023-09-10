namespace Memorial.Lib.Columbarium
{
    public interface ITransfer : ITransaction
    {
        bool AllowNicheDeceasePairing(int nicheId, int applicantId);
        bool Add(Core.Domain.ColumbariumTransaction columbariumTransaction);
        bool Change(string AF, Core.Domain.ColumbariumTransaction columbariumTransaction);
        bool Remove(string AF);
    }
}