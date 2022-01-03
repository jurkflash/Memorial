using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IUrnTransactionRepository : IRepository<UrnTransaction>
    {
        UrnTransaction GetActive(string AF);

        IEnumerable<UrnTransaction> GetByItem(int itemId, string filter);

        IEnumerable<UrnTransaction> GetByItemAndApplicant(int itemId, int applicantId);

        IEnumerable<UrnTransaction> GetRecent(int? number, int siteId, int? applicantId);
    }
}
