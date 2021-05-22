using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
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

        int GetTransactionAncestralTabletId();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        int GetTransactionApplicantId();

        int? GetTransactionDeceasedId();

        IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestralTabletIdAndItemId(int ancestralTabletId, int itemId, string filter);

        IEnumerable<AncestralTabletTransactionDto> GetTransactionDtosByAncestralTabletIdAndItemId(int ancestralTabletId, int itemId, string filter);

        IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestralTabletIdAndItemIdAndApplicantId(int ancestralTabletId, int itemId, int applicantId);

        IEnumerable<AncestralTabletTransactionDto> GetTransactionDtosByAncestralTabletIdAndItemIdAndApplicantId(int ancestralTabletId, int itemId, int applicantId);

        Core.Domain.AncestralTabletTransaction GetTransactionsByShiftedAncestralTabletTransactionAF(string AF);
    }
}