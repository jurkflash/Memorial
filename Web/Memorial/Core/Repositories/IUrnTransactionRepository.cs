using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IUrnTransactionRepository : IRepository<UrnTransaction>
    {
        UrnTransaction GetByAF(string AF);

        IEnumerable<UrnTransaction> GetByItem(int itemId, string filter);

        IEnumerable<UrnTransaction> GetByItemAndApplicant(int itemId, int applicantId);

        IEnumerable<UrnTransaction> GetRecent(int? number, byte? siteId, int? applicantId);

        bool GetExistsByApplicant(int id);
    }
}
