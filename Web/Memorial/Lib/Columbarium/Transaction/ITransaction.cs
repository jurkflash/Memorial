using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        float GetTransactionAmount();

        int GetTransactionQuadrangleId();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        int GetTransactionApplicantId();

        int? GetTransactionDeceased1Id();

        IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByQuadrangleIdAndItemId(int quadrangleId, int itemId, string filter);

        IEnumerable<ColumbariumTransactionDto> GetTransactionDtosByQuadrangleIdAndItemId(int quadrangleId, int itemId, string filter);

        IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId);

        IEnumerable<ColumbariumTransactionDto> GetTransactionDtosByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId);

        Core.Domain.ColumbariumTransaction GetTransactionsByShiftedQuadrangleTransactionAF(string AF);
    }
}