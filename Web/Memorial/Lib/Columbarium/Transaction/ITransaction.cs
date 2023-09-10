using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface ITransaction
    {
        Core.Domain.ColumbariumTransaction GetByAF(string AF);
        float GetTotalAmount(Core.Domain.ColumbariumTransaction columbariumTransaction);
        IEnumerable<Core.Domain.ColumbariumTransaction> GetByNicheIdAndItemId(int nicheId, int itemId, string filter);
        Core.Domain.ColumbariumTransaction GetByShiftedColumbariumTransactionAF(string AF);
        IEnumerable<Core.Domain.ColumbariumTransaction> GetRecent(int siteId, int? applicantId);
    }
}