using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IReceipt
    {
        Core.Domain.Receipt GetByRE(string RE);
        bool Remove(Core.Domain.Receipt receipt);


        void SetReceipt(string RE);

        IEnumerable<Core.Domain.Receipt> GetByIV(string IV);

        string GetInvoiceIV();

        bool isOrderReceipt();

        float GetTotalIssuedReceiptAmountByIV(string IV);

        bool DeleteByIV(string IV);
    }
}