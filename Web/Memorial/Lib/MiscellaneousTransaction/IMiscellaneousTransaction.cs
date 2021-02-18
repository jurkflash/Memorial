using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IMiscellaneousTransaction
    {
        IEnumerable<MiscellaneousTransactionDto> DtosGetByItemAndApplicant(int miscellaneousItemId, int applicantId);

        bool CreateNewTransaction(MiscellaneousTransactionDto miscellaneousTransactionDto);

        MiscellaneousTransactionDto GetTransactionDto(string AF);

        float GetAmount(string AF);

        float GetUnpaidNonOrderAmount(string AF);

        bool Delete(string AF);
    }
}