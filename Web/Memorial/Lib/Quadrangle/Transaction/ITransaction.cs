using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface ITransaction
    {
        void SetTransaction(string AF);

        void SetTransaction(Core.Domain.QuadrangleTransaction transaction);

        Core.Domain.QuadrangleTransaction GetTransaction();

        Core.Domain.QuadrangleTransaction GetTransactionExclusive(string AF);

        QuadrangleTransactionDto GetTransactionDto();

        Core.Domain.QuadrangleTransaction GetTransaction(string AF);

        QuadrangleTransactionDto GetTransactionDto(string AF);

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

        IEnumerable<Core.Domain.QuadrangleTransaction> GetTransactionsByQuadrangleIdAndItemId(int quadrangleId, int itemId, string filter);

        IEnumerable<QuadrangleTransactionDto> GetTransactionDtosByQuadrangleIdAndItemId(int quadrangleId, int itemId, string filter);

        IEnumerable<Core.Domain.QuadrangleTransaction> GetTransactionsByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId);

        IEnumerable<QuadrangleTransactionDto> GetTransactionDtosByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId);

        Core.Domain.QuadrangleTransaction GetTransactionsByShiftedQuadrangleTransactionAF(string AF);
    }
}