using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IUrn
    {
        IEnumerable<UrnDto> DtosGetBySite(byte siteId);

        IEnumerable<UrnItemDto> ItemDtosGetByUrn(int urnId);

        bool IsOrderFlag(int urnItemId);

        IEnumerable<UrnTransactionDto> TransactionDtosGetByItemAndApplicant(int urnItemId, int applicantId);

        bool CreateNewTransaction(UrnTransactionDto urnTransactionDto);

        UrnTransactionDto GetTransactionDto(string AF);

        float GetAmount(string AF);

        float GetUnpaidNonOrderAmount(string AF);

        bool Delete(string AF);
    }
}