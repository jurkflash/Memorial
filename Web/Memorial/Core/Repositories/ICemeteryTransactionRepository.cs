﻿using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICemeteryTransactionRepository : IRepository<CemeteryTransaction>
    {
        CemeteryTransaction GetByAF(string AF);

        CemeteryTransaction GetExclusive(string AF);

        IEnumerable<CemeteryTransaction> GetByApplicant(int id);

        bool GetExistsByApplicant(int id);

        bool GetExistsByDeceased(int id);

        IEnumerable<CemeteryTransaction> GetByPlotIdAndItem(int plotId, int itemId, string filter);

        CemeteryTransaction GetByPlotIdAndDeceased(int plotId, int deceased1Id);

        CemeteryTransaction GetLastCemeteryTransactionByPlotId(int plotId);

        CemeteryTransaction GetLastCemeteryTransactionByShiftedPlotId(int plotId);

        IEnumerable<CemeteryTransaction> GetByPlotId(int plotId);

        IEnumerable<CemeteryTransaction> GetByPlotIdAndItemAndApplicant(int plotId, int itemId, int applicantId);

        IEnumerable<CemeteryTransaction> GetRecent(int? number, byte? siteId, int? applicantId);

    }
}
