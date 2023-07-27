using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestralTabletTransactionRepository : IRepository<AncestralTabletTransaction>
    {
        AncestralTabletTransaction GetActive(string AF);

        AncestralTabletTransaction GetExclusive(string AF);

        IEnumerable<AncestralTabletTransaction> GetByApplicant(int id);

        bool GetExistsByApplicant(int id);

        bool GetExistsByDeceased(int id);

        IEnumerable<AncestralTabletTransaction> GetByAncestralTabletIdAndItem(int ancestralTabletId, int itemId, string filter);

        AncestralTabletTransaction GetByShiftedAncestralTabletTransactionAF(string AF);

        IEnumerable<AncestralTabletTransaction> GetByAncestralTabletId(int ancestralTabletId);

        IEnumerable<AncestralTabletTransaction> GetByAncestralTabletIdAndItemAndApplicant(int ancestralTabletId, int itemId, int applicantId);

        IEnumerable<AncestralTabletTransaction> GetRecent(int? number, int siteId, int? applicantId);
    }
}
