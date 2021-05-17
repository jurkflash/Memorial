using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public interface ITransaction
    {
        void SetTransaction(string AF);

        void SetTransaction(Core.Domain.PlotTransaction transaction);

        Core.Domain.PlotTransaction GetTransaction();

        PlotTransactionDto GetTransactionDto();

        Core.Domain.PlotTransaction GetTransaction(string AF);

        Core.Domain.PlotTransaction GetTransactionExclusive(string AF);

        PlotTransactionDto GetTransactionDto(string AF);

        string GetTransactionAF();

        float GetTransactionAmount();

        int GetTransactionPlotId();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        int GetTransactionApplicantId();

        int? GetTransactionDeceased1Id();

        IEnumerable<Core.Domain.PlotTransaction> GetTransactionsByPlotIdAndItemId(int plotId, int itemId, string filter);

        IEnumerable<PlotTransactionDto> GetTransactionDtosByPlotIdAndItemId(int plotId, int itemId, string filter);

        Core.Domain.PlotTransaction GetTransactionsByPlotIdAndDeceased1Id(int plotId, int deceased1Id);

        IEnumerable<Core.Domain.PlotTransaction> GetTransactionsByPlotIdAndItemIdAndApplicantId(int plotId, int itemId, int applicantId);

        IEnumerable<PlotTransactionDto> GetTransactionDtosByPlotIdAndItemIdAndApplicantId(int plotId, int itemId, int applicantId);

        Core.Domain.PlotTransaction GetLastPlotTransactionByPlotId(int plotId);
    }
}