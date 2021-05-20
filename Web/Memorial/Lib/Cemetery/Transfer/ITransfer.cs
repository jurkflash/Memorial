using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public interface ITransfer : ITransaction
    {
        void SetTransfer(string AF);

        bool AllowPlotDeceasePairing(IPlot plot, int applicantId);

        bool Create(CemeteryTransactionDto plotTransactionDto);

        bool Update(CemeteryTransactionDto plotTransactionDto);

        bool Delete();
    }
}