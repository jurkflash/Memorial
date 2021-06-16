using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICremationTransactionRepository : IRepository<CremationTransaction>
    {
        CremationTransaction GetActive(string AF);

        IEnumerable<CremationTransaction> GetByItem(int itemId, string filter);

        IEnumerable<CremationTransaction> GetByItemAndDeceased(int itemId, int deceasedId);

        IEnumerable<CremationTransaction> GetByApplicant(int id);

        IEnumerable<CremationTransaction> GetByItemAndApplicant(int itemId, int applicantId);

        IEnumerable<CremationTransaction> GetRecent(int number, int siteId);
    }
}
