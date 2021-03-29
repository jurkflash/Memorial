using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorTransactionRepository : IRepository<AncestorTransaction>
    {
        AncestorTransaction GetActive(string AF);

        IEnumerable<AncestorTransaction> GetByApplicant(int id);

        IEnumerable<AncestorTransaction> GetByAncestorIdAndItem(int ancestorId, int itemId);

        IEnumerable<AncestorTransaction> GetByItemAndApplicant(int itemId, int applicantId);

        IEnumerable<AncestorTransaction> GetByAncestorIdAndItemAndApplicant(int ancestorId, int itemId, int applicantId);

        AncestorTransaction GetLastAncestorTransactionByAncestorId(int ancestorId);

        AncestorTransaction GetLastAncestorTransactionByShiftedAncestorId(int ancestorId);
    }
}
