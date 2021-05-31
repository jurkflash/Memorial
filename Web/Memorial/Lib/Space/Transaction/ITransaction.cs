using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib.Space
{
    public interface ITransaction
    {
        void SetTransaction(string AF);

        void SetTransaction(Core.Domain.SpaceTransaction transaction);

        Core.Domain.SpaceTransaction GetTransaction();

        SpaceTransactionDto GetTransactionDto();

        Core.Domain.SpaceTransaction GetTransaction(string AF);

        SpaceTransactionDto GetTransactionDto(string AF);

        string GetTransactionAF();

        float GetTransactionAmount();

        float GetTransactionOtherCharges();

        float GetTransactionTotalAmount();

        string GetTransactionSummaryItem();

        int GetTransactionSpaceItemId();

        string GetSiteHeader();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        bool IsItemAllowDeposit();

        int GetTransactionApplicantId();

        int? GetTransactionDeceasedId();

        IEnumerable<Core.Domain.SpaceTransaction> GetTransactionsByItemId(int itemId, string filter);

        IEnumerable<SpaceTransactionDto> GetTransactionDtosByItemId(int itemId, string filter);

        IEnumerable<Core.Domain.SpaceTransaction> GetTransactionsByItemIdAndApplicantId(int applicantId, int itemId);

        IEnumerable<SpaceTransactionDto> GetTransactionDtosByItemIdAndApplicantId(int applicantId, int itemId);

        IEnumerable<Core.Domain.SpaceTransaction> GetTransactionByItemIdAndDeceasedId(int deceasedId, int itemId);

        IEnumerable<Core.Domain.SpaceBooked> GetBookedTransaction(DateTime from, DateTime to, byte siteId);

    }
}