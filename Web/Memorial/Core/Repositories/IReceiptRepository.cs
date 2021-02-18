using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IReceiptRepository : IRepository<Receipt>
    {
        IEnumerable<Receipt> GetByNonOrderActiveAncestorAF(string AF);

        IEnumerable<Receipt> GetByNonOrderActiveCremationAF(string AF);

        IEnumerable<Receipt> GetByNonOrderActivePlotAF(string AF);

        IEnumerable<Receipt> GetByNonOrderActiveSpaceAF(string AF);

        IEnumerable<Receipt> GetByNonOrderActiveUrnAF(string AF);

        IEnumerable<Receipt> GetByNonOrderActiveQuadrangleAF(string AF);

        IEnumerable<Receipt> GetByNonOrderActiveMiscellaneousAF(string AF);

        IEnumerable<Receipt> GetByActiveIV(string IV);

        Receipt GetByActiveRE(string RE);
    }
}
