using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IQuadrangleTransaction
    {
        void SetByAF(string AF);

        QuadrangleTransactionDto GetDto();

        IEnumerable<Core.Domain.QuadrangleTransaction> GetByItemAndApplicant(int itemId, int applicantId);

        IEnumerable<QuadrangleTransactionDto> DtosGetByItemAndApplicant(int itemId, int applicantId);

        bool CreateNew(QuadrangleTransactionDto quadrangleTransactionDto);

        float GetAmount();

        float GetUnpaidNonOrderAmount();

        bool Delete();

    }
}