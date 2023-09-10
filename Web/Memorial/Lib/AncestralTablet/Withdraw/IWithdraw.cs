namespace Memorial.Lib.AncestralTablet
{
    public interface IWithdraw : ITransaction
    {
        bool Add(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction);
        bool Change(string AF, Core.Domain.AncestralTabletTransaction ancestralTabletTransaction);
        bool Remove(string AF);
        bool RemoveWithdrew(int ancestralTabletId);
    }
}