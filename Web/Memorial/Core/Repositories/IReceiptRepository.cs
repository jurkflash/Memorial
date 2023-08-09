using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IReceiptRepository : IRepository<Receipt>
    {
        IEnumerable<Receipt> GetByAncestralTabletAF(string AF, bool isOrder);

        IEnumerable<Receipt> GetByCremationAF(string AF, bool isOrder);

        IEnumerable<Receipt> GetByCemeteryAF(string AF, bool isOrder);

        IEnumerable<Receipt> GetBySpaceAF(string AF);

        IEnumerable<Receipt> GetByUrnAF(string AF, bool isOrder);

        IEnumerable<Receipt> GetByColumbariumAF(string AF, bool isOrder);

        IEnumerable<Receipt> GetByMiscellaneousAF(string AF, bool isOrder);

        float GetTotalAmountByAncestralTabletAF(string AF);

        float GetTotalAmountByCremationAF(string AF);

        float GetTotalAmountByCemeteryAF(string AF);

        float GetTotalAmountBySpaceAF(string AF);

        float GetTotalAmountByUrnAF(string AF);

        float GetTotalAmountByColumbariumAF(string AF);

        float GetTotalAmountByMiscellaneousAF(string AF);

        IEnumerable<Receipt> GetByIV(string IV);

        Receipt GetByRE(string RE);
    }
}
