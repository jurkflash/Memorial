using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        IEnumerable<Invoice> GetByActiveAncestorAF(string AF);

        IEnumerable<Invoice> GetByActiveCremationAF(string AF);

        IEnumerable<Invoice> GetByActivePlotAF(string AF);

        IEnumerable<Invoice> GetByActiveSpaceAF(string AF);

        IEnumerable<Invoice> GetByActiveUrnAF(string AF);

        IEnumerable<Invoice> GetByActiveColumbariumAF(string AF);

        IEnumerable<Invoice> GetByActiveMiscellaneousAF(string AF);

        Invoice GetByActiveIV(string IV);
    }
}
