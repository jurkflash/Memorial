using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleTransactionRepository : IRepository<QuadrangleTransaction>
    {
        QuadrangleTransaction GetActive(string AF);

        IEnumerable<QuadrangleTransaction> GetByApplicant(int id);

        IEnumerable<QuadrangleTransaction> GetByQuadrangleIdAndItem(int quadrangleId, int itemId);

        QuadrangleTransaction GetLastQuadrangleTransactionByQuadrangleId(int quadrangleId);

        QuadrangleTransaction GetLastQuadrangleTransactionByShiftedQuadrangleId(int quadrangleId);

        IEnumerable<QuadrangleTransaction> GetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId);
    }
}
