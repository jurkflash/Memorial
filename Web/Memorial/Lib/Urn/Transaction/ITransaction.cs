using System.Collections.Generic;

namespace Memorial.Lib.Urn
{
    public interface ITransaction
    {
        Core.Domain.UrnTransaction GetByAF(string AF);
        float GetTotalAmount(Core.Domain.UrnTransaction urnTransaction);
        IEnumerable<Core.Domain.UrnTransaction> GetByItemId(int itemId, string filter);
        IEnumerable<Core.Domain.UrnTransaction> GetRecent(int siteId, int? applicantId);
    }
}