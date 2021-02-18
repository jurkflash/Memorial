using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IMiscellaneousTransactionRepository : IRepository<MiscellaneousTransaction>
    {
        MiscellaneousTransaction GetActive(string AF);

        IEnumerable<MiscellaneousTransaction> GetByApplicant(int id);

        IEnumerable<MiscellaneousTransaction> GetByItemAndApplicant(int itemId, int applicantId);
    }
}
