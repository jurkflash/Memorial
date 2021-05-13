using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorTransactionRepository : IRepository<AncestorTransaction>
    {
        AncestorTransaction GetActive(string AF);

        AncestorTransaction GetExclusive(string AF);

        IEnumerable<AncestorTransaction> GetByApplicant(int id);

        IEnumerable<AncestorTransaction> GetByAncestorIdAndItem(int ancestorId, int itemId);

        AncestorTransaction GetByShiftedAncestorTransactionAF(string AF);

        IEnumerable<AncestorTransaction> GetByAncestorIdAndItemAndApplicant(int ancestorId, int itemId, int applicantId);
    }
}
