using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Urn
{
    public interface ITransaction
    {
        void SetTransaction(string AF);

        void SetTransaction(Core.Domain.UrnTransaction transaction);

        Core.Domain.UrnTransaction GetTransaction();

        UrnTransactionDto GetTransactionDto();

        Core.Domain.UrnTransaction GetTransaction(string AF);

        UrnTransactionDto GetTransactionDto(string AF);

        string GetTransactionAF();

        float GetTransactionAmount();

        string GetTransactionSummaryItem();

        string GetSiteHeader();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        int GetTransactionApplicantId();

        IEnumerable<Core.Domain.UrnTransaction> GetTransactionsByItemId(int itemId, string filter);

        IEnumerable<UrnTransactionDto> GetTransactionDtosByItemId(int itemId, string filter);

        IEnumerable<UrnTransactionDto> GetRecent(int? number, int siteId);
    }
}