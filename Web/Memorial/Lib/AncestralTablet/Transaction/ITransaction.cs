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

        void SetTransaction(Core.Domain.AncestralTabletTransaction transaction);

        Core.Domain.AncestralTabletTransaction GetTransaction();

        Core.Domain.AncestralTabletTransaction GetTransactionExclusive(string AF);

        AncestralTabletTransactionDto GetTransactionDto();

        Core.Domain.AncestralTabletTransaction GetTransaction(string AF);

        AncestralTabletTransactionDto GetTransactionDto(string AF);

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

        IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestorIdAndItemId(int ancestorId, int itemId, string filter);

        IEnumerable<AncestralTabletTransactionDto> GetTransactionDtosByAncestorIdAndItemId(int ancestorId, int itemId, string filter);

        IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestorIdAndItemIdAndApplicantId(int ancestorId, int itemId, int applicantId);

        IEnumerable<AncestralTabletTransactionDto> GetTransactionDtosByAncestorIdAndItemIdAndApplicantId(int ancestorId, int itemId, int applicantId);

        Core.Domain.AncestralTabletTransaction GetTransactionsByShiftedAncestralTabletTransactionAF(string AF);
    }
}