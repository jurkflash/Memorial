using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IMiscellaneousTransactionRepository : IRepository<MiscellaneousTransaction>
    {
        MiscellaneousTransaction GetActive(string AF);

        IEnumerable<MiscellaneousTransaction> GetByItem(int itemId, string filter);

        IEnumerable<MiscellaneousTransaction> GetByItemAndApplicant(int itemId, int applicantId);

        IEnumerable<MiscellaneousTransaction> GetRecent(int number, int siteId);
    }
}
