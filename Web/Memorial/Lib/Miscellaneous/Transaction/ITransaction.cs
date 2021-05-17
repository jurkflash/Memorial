using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Miscellaneous
{
    public interface ITransaction
    {
        void SetTransaction(string AF);

        void SetTransaction(Core.Domain.MiscellaneousTransaction transaction);

        Core.Domain.MiscellaneousTransaction GetTransaction();

        MiscellaneousTransactionDto GetMiscellaneousDto();

        Core.Domain.MiscellaneousTransaction GetTransaction(string AF);

        MiscellaneousTransactionDto GetTransactionDto(string AF);

        string GetTransactionAF();

        float GetTransactionAmount();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        int? GetTransactionApplicantId();

        IEnumerable<Core.Domain.MiscellaneousTransaction> GetTransactionsByItemId(int itemId, string filter);

        IEnumerable<MiscellaneousTransactionDto> GetTransactionDtosByItemId(int itemId, string filter);
    }
}