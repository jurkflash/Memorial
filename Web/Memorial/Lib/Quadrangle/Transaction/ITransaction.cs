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

        void SetTransaction(Core.Domain.QuadrangleTransaction quadrangleTransaction);

        Core.Domain.QuadrangleTransaction GetTransaction();

        QuadrangleTransactionDto GetTransactionDto();

        Core.Domain.QuadrangleTransaction GetTransaction(string AF);

        QuadrangleTransactionDto GetTransactionDto(string AF);

        string GetAF();

        float GetAmount();

        int GetQuadrangleId();

        int GetItemId();

        string GetItemName();

        int GetApplicantId();

        int? GetDeceasedId();

        IEnumerable<Core.Domain.QuadrangleTransaction> GetTransactionsByQuadrangleIdAndItemId(int quadrangleId, int itemId);

        IEnumerable<QuadrangleTransactionDto> GetTransactionDtosByQuadrangleIdAndItemId(int quadrangleId, int itemId);

        IEnumerable<Core.Domain.QuadrangleTransaction> GetTransactionsByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId);

        IEnumerable<QuadrangleTransactionDto> GetTransactionDtosByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId);

        float GetUnpaidNonOrderAmount();

        bool Delete();

    }
}