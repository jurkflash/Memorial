using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface ITransfer : ITransaction
    {
        void SetTransfer(string AF);

        bool AllowQuadrangleDeceasePairing(IQuadrangle quadrangle, int applicantId);

        bool Create(QuadrangleTransactionDto quadrangleTransactionDto);

        bool Update(QuadrangleTransactionDto quadrangleTransactionDto);

        bool Delete();
    }
}