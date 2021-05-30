using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface ITransaction
    {
        void SetTransaction(string AF);

        void SetTransaction(Core.Domain.ColumbariumTransaction transaction);

        Core.Domain.ColumbariumTransaction GetTransaction();

        Core.Domain.ColumbariumTransaction GetTransactionExclusive(string AF);

        ColumbariumTransactionDto GetTransactionDto();

        Core.Domain.ColumbariumTransaction GetTransaction(string AF);

        ColumbariumTransactionDto GetTransactionDto(string AF);

        string GetTransactionAF();

        float GetTransactionTotalAmount();

        int GetTransactionNicheId();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        int GetTransactionApplicantId();

        int? GetTransactionDeceased1Id();

        IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByNicheIdAndItemId(int nicheId, int itemId, string filter);

        IEnumerable<ColumbariumTransactionDto> GetTransactionDtosByNicheIdAndItemId(int nicheId, int itemId, string filter);

        IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByNicheIdAndItemIdAndApplicantId(int nicheId, int itemId, int applicantId);

        IEnumerable<ColumbariumTransactionDto> GetTransactionDtosByNicheIdAndItemIdAndApplicantId(int nicheId, int itemId, int applicantId);

        Core.Domain.ColumbariumTransaction GetTransactionsByShiftedColumbariumTransactionAF(string AF);
    }
}