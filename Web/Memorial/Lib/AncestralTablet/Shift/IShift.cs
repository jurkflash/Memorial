using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
{
    public interface IShift : ITransaction
    {
        bool Add(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction);
        bool Change(string AF, Core.Domain.AncestralTabletTransaction ancestralTabletTransaction);
        bool Remove(string AF);
    }
}