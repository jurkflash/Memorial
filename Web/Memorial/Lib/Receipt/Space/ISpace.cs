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
        bool Change(string RE, Core.Domain.Receipt receipt);
        bool Add(int itemId, Core.Domain.Receipt receipt);
        IEnumerable<Core.Domain.Receipt> GetByAF(string AF);






        string GetApplicationAF();

        float GetTotalIssuedReceiptAmount(string AF);

        bool Create(int itemId, ReceiptDto receiptDto);

        bool Update(ReceiptDto receiptDto);

        bool Delete();

        bool DeleteNonOrderReceiptsByApplicationAF(string AF);
    }
}