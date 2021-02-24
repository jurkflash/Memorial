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

        Core.Domain.Quadrangle GetQuadrangle();

        QuadrangleDto DtoGetQuadrangle();

        float GetAmount();

        Core.Domain.QuadrangleTransaction GetTransaction();

        QuadrangleTransactionDto DtoGetTransaction();

        string GetItemName();

        int GetApplicantId();

        Core.Domain.Applicant GetApplicant();

        ApplicantDto DtoGetApplicant();

        int? GetDeceasedId();

        Core.Domain.Applicant GetDeceased();

        DeceasedDto DtoGetDeceased();

        IEnumerable<Core.Domain.QuadrangleTransaction> GetByQuadrangleIdAndItem(int quadrangleId, int itemId);

        IEnumerable<QuadrangleTransactionDto> DtosGetByQuadrangleIdAndItem(int quadrangleId, int itemId);

        IEnumerable<Core.Domain.QuadrangleTransaction> GetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId);

        IEnumerable<QuadrangleTransactionDto> DtosGetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId);

        float GetUnpaidNonOrderAmount();

        bool Delete();

    }
}