using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public interface ISingleOrder : ITransaction
    {
        void SetSingleOrder(string AF);

        bool Create(PlotTransactionDto plotTransactionDto);

        bool Update(PlotTransactionDto plotTransactionDto);

        bool Delete();
    }
}