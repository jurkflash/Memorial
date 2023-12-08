using System.Collections.Generic;

namespace Memorial.Lib.AncestralTablet
{
    public interface ITransaction
    {
        Core.Domain.AncestralTabletTransaction GetByAF(string AF);
        Core.Domain.AncestralTabletTransaction GetExclusive(string AF);
        float GetTotalAmount(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction);
        IEnumerable<Core.Domain.AncestralTabletTransaction> GetByAncestralTabletIdAndItemId(int ancestralTabletId, int itemId, string filter);
        Core.Domain.AncestralTabletTransaction GetTransactionsByShiftedAncestralTabletTransactionAF(string AF);
        IEnumerable<Core.Domain.AncestralTabletTransaction> GetRecent(byte? siteId, int? applicantId);
    }
}