using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorTransactionRepository : IRepository<AncestorTransaction>
    {
        AncestorTransaction GetActive(string AF);

        IEnumerable<AncestorTransaction> GetByApplicant(int id);

        IEnumerable<AncestorTransaction> GetByItemAndApplicant(int itemId, int applicantId);
    }
}
