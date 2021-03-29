using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;
using AutoMapper;

namespace Memorial.Lib.Receipt
{
    public interface ISpace : IReceipt
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