using System.Collections.Generic;

namespace Memorial.Lib.Cremation
{
    public interface ITransaction
    {
        Core.Domain.CremationTransaction GetByAF(string AF);
        float GetTotalAmount(Core.Domain.CremationTransaction transaction);
        IEnumerable<Core.Domain.CremationTransaction> GetByItemId(int itemId, string filter);
        IEnumerable<Core.Domain.CremationTransaction> GetByItemIdAndDeceasedId(int itemId, int deceasedId);
        IEnumerable<Core.Domain.CremationTransaction> GetRecent(byte? siteId, int? applicantId);
    }
}