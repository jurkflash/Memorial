using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICremationTransactionRepository : IRepository<CremationTransaction>
    {
        CremationTransaction GetActive(string AF);

        IEnumerable<CremationTransaction> GetByApplicant(int id);

        IEnumerable<CremationTransaction> GetByItemAndApplicant(int itemId, int applicantId);
    }
}
