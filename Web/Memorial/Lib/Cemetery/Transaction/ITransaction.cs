using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public interface ITransaction
    {
        Core.Domain.CemeteryTransaction GetByAF(string AF);
        float GetTotalAmount(Core.Domain.CemeteryTransaction cemeteryTransaction);
        Core.Domain.CemeteryTransaction GetTransactionExclusive(string AF);
        IEnumerable<Core.Domain.CemeteryTransaction> GetTransactionsByPlotIdAndItemId(int plotId, int itemId, string filter);
        IEnumerable<CemeteryTransactionDto> GetTransactionDtosByPlotIdAndItemId(int plotId, int itemId, string filter);
        Core.Domain.CemeteryTransaction GetTransactionsByPlotIdAndDeceased1Id(int plotId, int deceased1Id);
        Core.Domain.CemeteryTransaction GetLastCemeteryTransactionTransactionByPlotId(int plotId);
        IEnumerable<Core.Domain.CemeteryTransaction> GetRecent(int siteId, int? applicantId);
    }
}