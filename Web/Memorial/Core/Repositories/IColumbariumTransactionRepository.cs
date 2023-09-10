using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IColumbariumTransactionRepository : IRepository<ColumbariumTransaction>
    {
        ColumbariumTransaction GetByAF(string AF);

        ColumbariumTransaction GetExclusive(string AF);

        IEnumerable<ColumbariumTransaction> GetByApplicant(int id);

        bool GetExistsByApplicant(int id);

        bool GetExistsByDeceased(int id);

        IEnumerable<ColumbariumTransaction> GetByNicheIdAndItem(int nicheId, int itemId, string filter);

        ColumbariumTransaction GetByShiftedColumbariumTransactionAF(string AF);

        IEnumerable<ColumbariumTransaction> GetByNicheId(int nicheId);

        IEnumerable<ColumbariumTransaction> GetByNicheIdAndItemAndApplicant(int nicheId, int itemId, int applicantId);

        IEnumerable<ColumbariumTransaction> GetRecent(int? number, int siteId, int? applicantId);
    }
}
