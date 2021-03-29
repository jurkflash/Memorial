using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface ITransaction
    {
        void SetTransaction(string AF);

        void SetTransaction(Core.Domain.AncestorTransaction transaction);

        Core.Domain.AncestorTransaction GetTransaction();

        AncestorTransactionDto GetTransactionDto();

        Core.Domain.AncestorTransaction GetTransaction(string AF);

        AncestorTransactionDto GetTransactionDto(string AF);

        string GetTransactionAF();

        float GetTransactionAmount();

        int GetTransactionAncestorId();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        int GetTransactionApplicantId();

        int? GetTransactionDeceasedId();

        IEnumerable<Core.Domain.AncestorTransaction> GetTransactionsByAncestorIdAndItemId(int ancestorId, int itemId);

        IEnumerable<AncestorTransactionDto> GetTransactionDtosByAncestorIdAndItemId(int ancestorId, int itemId);

        IEnumerable<Core.Domain.AncestorTransaction> GetTransactionsByAncestorIdAndItemIdAndApplicantId(int ancestorId, int itemId, int applicantId);

        IEnumerable<AncestorTransactionDto> GetTransactionDtosByAncestorIdAndItemIdAndApplicantId(int ancestorId, int itemId, int applicantId);

        Core.Domain.AncestorTransaction GetLastAncestorTransactionByAncestorId(int ancestorId);

        Core.Domain.AncestorTransaction GetLastAncestorTransactionByShiftedAncestorId(int ancestorId);
    }
}