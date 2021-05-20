using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public interface ITransaction
    {
        void SetTransaction(string AF);

        void SetTransaction(Core.Domain.CemeteryTransaction transaction);

        Core.Domain.CemeteryTransaction GetTransaction();

        CemeteryTransactionDto GetTransactionDto();

        Core.Domain.CemeteryTransaction GetTransaction(string AF);

        Core.Domain.CemeteryTransaction GetTransactionExclusive(string AF);

        CemeteryTransactionDto GetTransactionDto(string AF);

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

        IEnumerable<Core.Domain.CemeteryTransaction> GetTransactionsByPlotIdAndItemId(int plotId, int itemId, string filter);

        IEnumerable<CemeteryTransactionDto> GetTransactionDtosByPlotIdAndItemId(int plotId, int itemId, string filter);

        Core.Domain.CemeteryTransaction GetTransactionsByPlotIdAndDeceased1Id(int plotId, int deceased1Id);

        IEnumerable<Core.Domain.CemeteryTransaction> GetTransactionsByPlotIdAndItemIdAndApplicantId(int plotId, int itemId, int applicantId);

        IEnumerable<CemeteryTransactionDto> GetTransactionDtosByPlotIdAndItemIdAndApplicantId(int plotId, int itemId, int applicantId);

        Core.Domain.CemeteryTransaction GetLastPlotTransactionByPlotId(int plotId);
    }
}