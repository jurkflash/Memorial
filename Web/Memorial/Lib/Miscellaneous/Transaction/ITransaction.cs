using System.Collections.Generic;

namespace Memorial.Lib.Miscellaneous
{
    public interface ITransaction
    {
        Core.Domain.MiscellaneousTransaction GetByAF(string AF);
        float GetTotalAmount(Core.Domain.MiscellaneousTransaction miscellaneousTransaction);
        IEnumerable<Core.Domain.MiscellaneousTransaction> GetByItemId(int itemId, string filter);
        IEnumerable<Core.Domain.MiscellaneousTransaction> GetRecent(int siteId, int? applicantId);
    }
}