using System;
using System.Collections.Generic;

namespace Memorial.Lib.Space
{
    public interface ITransaction
    {
        Core.Domain.SpaceTransaction GetByAF(string AF);
        IEnumerable<Core.Domain.SpaceTransaction> GetByItemId(int itemId, string filter);
        float GetTotalAmount(Core.Domain.SpaceTransaction spaceTransaction);
        IEnumerable<Core.Domain.SpaceTransaction> GetRecent(int siteId, int? applicantId);
        IEnumerable<Core.Domain.SpaceBooked> GetBookedTransaction(DateTime from, DateTime to, int siteId);
    }
}