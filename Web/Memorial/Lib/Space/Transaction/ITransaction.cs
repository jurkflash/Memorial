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

        int GetTransactionSpaceItemId();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        int GetTransactionApplicantId();

        int? GetTransactionDeceasedId();

        IEnumerable<Core.Domain.SpaceTransaction> GetTransactionsByItemId(int itemId);

        IEnumerable<SpaceTransactionDto> GetTransactionDtosByItemId(int itemId);

        IEnumerable<Core.Domain.SpaceTransaction> GetTransactionsByItemIdAndApplicantId(int applicantId, int itemId);

        IEnumerable<SpaceTransactionDto> GetTransactionDtosByItemIdAndApplicantId(int applicantId, int itemId);

        IEnumerable<Core.Domain.SpaceTransaction> GetTransactionByItemIdAndDeceasedId(int deceasedId, int itemId);

    }
}