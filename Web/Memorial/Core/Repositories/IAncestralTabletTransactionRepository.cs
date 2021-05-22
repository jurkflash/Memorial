using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestralTabletTransactionRepository : IRepository<AncestralTabletTransaction>
    {
        AncestralTabletTransaction GetActive(string AF);

        AncestralTabletTransaction GetExclusive(string AF);

        IEnumerable<AncestralTabletTransaction> GetByApplicant(int id);

        IEnumerable<AncestralTabletTransaction> GetByAncestorIdAndItem(int ancestorId, int itemId, string filter);

        AncestralTabletTransaction GetByShiftedAncestralTabletTransactionAF(string AF);

        IEnumerable<AncestralTabletTransaction> GetByAncestorId(int ancestorId);

        IEnumerable<AncestralTabletTransaction> GetByAncestorIdAndItemAndApplicant(int ancestorId, int itemId, int applicantId);
    }
}
