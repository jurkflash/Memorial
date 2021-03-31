using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public interface ITransfer : ITransaction
    {
        void SetTransfer(string AF);

        bool AllowPlotDeceasePairing(IPlot plot, int applicantId);

        bool Create(PlotTransactionDto plotTransactionDto);

        bool Update(PlotTransactionDto plotTransactionDto);

        bool Delete();
    }
}