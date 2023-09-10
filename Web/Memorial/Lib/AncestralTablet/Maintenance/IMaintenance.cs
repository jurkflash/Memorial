using System;

namespace Memorial.Lib.AncestralTablet
{
    public interface IMaintenance : ITransaction
    {
        bool ChangeAncestralTablet(int oldAncestralTabletId, int newAncestralTabletId);
        float GetAmount(int itemId, DateTime from, DateTime to);
        bool Add(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction);
        bool Change(string AF, Core.Domain.AncestralTabletTransaction ancestralTabletTransaction);
        bool Remove(string AF);
    }
}