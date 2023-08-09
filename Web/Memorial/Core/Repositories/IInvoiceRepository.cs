using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        IEnumerable<Invoice> GetByActiveAncestralTabletAF(string AF);

        IEnumerable<Invoice> GetByActiveCremationAF(string AF);

        IEnumerable<Invoice> GetByActiveCemeteryAF(string AF);

        IEnumerable<Invoice> GetByActiveSpaceAF(string AF);

        IEnumerable<Invoice> GetByActiveUrnAF(string AF);

        IEnumerable<Invoice> GetByActiveColumbariumAF(string AF);

        IEnumerable<Invoice> GetByActiveMiscellaneousAF(string AF);

        Invoice GetByIV(string IV);
    }
}
