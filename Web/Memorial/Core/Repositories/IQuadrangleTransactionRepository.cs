﻿using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleTransactionRepository : IRepository<QuadrangleTransaction>
    {
        QuadrangleTransaction GetActive(string AF);

        QuadrangleTransaction GetExclusive(string AF);

        IEnumerable<QuadrangleTransaction> GetByApplicant(int id);

        IEnumerable<QuadrangleTransaction> GetByQuadrangleIdAndItem(int quadrangleId, int itemId);

        QuadrangleTransaction GetByShiftedQuadrangleTransactionAF(string AF);

        QuadrangleTransaction GetLastQuadrangleTransactionByQuadrangleId(int quadrangleId);

        QuadrangleTransaction GetLastQuadrangleTransactionByShiftedQuadrangleId(int quadrangleId);

        IEnumerable<QuadrangleTransaction> GetQuadrangleTransactionsByMaintenanceShiftedQuadrangleId(int quadrangleId);

        IEnumerable<QuadrangleTransaction> GetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId);
    }
}
