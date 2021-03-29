using Memorial.Core;
using System;
using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Receipt
{
    public interface IQuadrangle : IReceipt
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