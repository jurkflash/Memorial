using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface ICremation
    {
        IEnumerable<CremationDto> DtosGetBySite(byte siteId);

        IEnumerable<CremationItemDto> ItemDtosGetByCremation(int cremationId);

        bool IsOrderFlag(int cremationItemId);

        IEnumerable<CremationTransactionDto> TransactionDtosGetByItemAndApplicant(int cremationItemId, int applicantId);

        bool CreateNewTransaction(CremationTransactionDto cremationTransactionDto);

        CremationTransactionDto GetTransactionDto(string AF);

        float GetAmount(string AF);

        float GetUnpaidNonOrderAmount(string AF);

        bool Delete(string AF);
    }
}