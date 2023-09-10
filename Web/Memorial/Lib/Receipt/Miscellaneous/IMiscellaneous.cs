using System.Collections.Generic;

namespace Memorial.Lib.Receipt
{
    public interface IMiscellaneous : IReceipt
    {
        bool Add(int itemId, Core.Domain.Receipt receipt);
        bool Change(string RE, Core.Domain.Receipt receipt);
        IEnumerable<Core.Domain.Receipt> GetByAF(string AF);
        float GetTotalIssuedReceiptAmountByAF(string AF);
    }
}