using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumTransactionRepository : IRepository<ColumbariumTransaction>
    {
        ColumbariumTransaction GetActive(string AF);

        ColumbariumTransaction GetExclusive(string AF);

        IEnumerable<ColumbariumTransaction> GetByApplicant(int id);

        IEnumerable<ColumbariumTransaction> GetByNicheIdAndItem(int nicheId, int itemId, string filter);

        ColumbariumTransaction GetByShiftedColumbariumTransactionAF(string AF);

        IEnumerable<ColumbariumTransaction> GetByNicheId(int nicheId);

        IEnumerable<ColumbariumTransaction> GetByNicheIdAndItemAndApplicant(int nicheId, int itemId, int applicantId);
    }
}
