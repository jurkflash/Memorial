using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumTransactionRepository : IRepository<ColumbariumTransaction>
    {
        ColumbariumTransaction GetActive(string AF);

        ColumbariumTransaction GetExclusive(string AF);

        IEnumerable<ColumbariumTransaction> GetByApplicant(int id);

        IEnumerable<ColumbariumTransaction> GetByQuadrangleIdAndItem(int quadrangleId, int itemId, string filter);

        ColumbariumTransaction GetByShiftedColumbariumTransactionAF(string AF);

        IEnumerable<ColumbariumTransaction> GetByQuadrangleId(int quadrangleId);

        IEnumerable<ColumbariumTransaction> GetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId);
    }
}
