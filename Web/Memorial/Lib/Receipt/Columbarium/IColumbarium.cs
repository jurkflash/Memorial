using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Receipt
{
    public interface IColumbarium : IReceipt
    {
        IEnumerable<Core.Domain.Receipt> GetNonOrderReceipts(string AF);

        IEnumerable<ReceiptDto> GetNonOrderReceiptDtos(string AF);

        string GetApplicationAF();

        float GetTotalIssuedNonOrderReceiptAmount(string AF);

        bool Create(int itemId, ReceiptDto receiptDto);

        bool Update(ReceiptDto receiptDto);

        bool Delete();

        bool DeleteNonOrderReceiptsByApplicationAF(string AF);
    }
}