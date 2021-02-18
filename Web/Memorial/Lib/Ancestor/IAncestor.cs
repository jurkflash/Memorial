using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IAncestor
    {
        IEnumerable<AncestorDto> DtosGetByArea(int areaId);

        IEnumerable<AncestorAreaDto> AreaDtosGetBySite(byte siteId);

        IEnumerable<AncestorItemDto> ItemDtosGetByArea(int areaId);

        bool IsOrderFlag(int ancestorItemId);

        IEnumerable<AncestorTransactionDto> TransactionDtosGetByItemAndApplicant(int ancestorItemId, int applicantId);

        bool CreateNewTransaction(AncestorTransactionDto ancestorTransactionDto);

        AncestorTransactionDto GetTransactionDto(string AF);

        float GetAmount(string AF);

        float GetUnpaidNonOrderAmount(string AF);

        bool Delete(string AF);
    }
}