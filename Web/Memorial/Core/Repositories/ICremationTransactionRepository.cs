using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICremationTransactionRepository : IRepository<CremationTransaction>
    {
        CremationTransaction GetByAF(string AF);

        IEnumerable<CremationTransaction> GetByItem(int itemId, string filter);

        IEnumerable<CremationTransaction> GetByItemAndDeceased(int itemId, int deceasedId);

        IEnumerable<CremationTransaction> GetByApplicant(int id);

        bool GetExistsByApplicant(int id);

        bool GetExistsByDeceased(int id);

        IEnumerable<CremationTransaction> GetByItemAndApplicant(int itemId, int applicantId);

        IEnumerable<CremationTransaction> GetRecent(int? number, byte? siteId, int? applicantId);
    }
}
