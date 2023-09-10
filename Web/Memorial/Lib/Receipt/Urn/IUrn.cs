using System.Collections.Generic;

namespace Memorial.Lib.Receipt
{
    public interface IUrn : IReceipt
    {
        IEnumerable<Core.Domain.Receipt> GetByAF(string AF);
        bool Change(string RE, Core.Domain.Receipt receipt);
        float GetTotalIssuedReceiptAmountByAF(string AF);
        bool Add(int itemId, Core.Domain.Receipt receipt);
    }
}