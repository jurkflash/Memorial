using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<Invoice> GetByActiveAncestralTabletAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.AncestralTabletTransactionAF == AF)
                .ToList().OrderBy(i => i.CreatedUtcTime);
        }

        public IEnumerable<Invoice> GetByActiveCremationAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.CremationTransactionAF == AF)
                            .ToList().OrderBy(i => i.CreatedUtcTime);
        }

        public IEnumerable<Invoice> GetByActiveCemeteryAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.CemeteryTransactionAF == AF)
                            .ToList().OrderBy(i => i.CreatedUtcTime);
        }

        public IEnumerable<Invoice> GetByActiveSpaceAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.SpaceTransactionAF == AF)
                            .ToList().OrderBy(i => i.CreatedUtcTime);
        }

        public IEnumerable<Invoice> GetByActiveUrnAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.UrnTransactionAF == AF)
                            .ToList().OrderBy(i => i.CreatedUtcTime);
        }

        public IEnumerable<Invoice> GetByActiveColumbariumAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.ColumbariumTransactionAF == AF)
                            .ToList().OrderBy(i => i.CreatedUtcTime);
        }

        public IEnumerable<Invoice> GetByActiveMiscellaneousAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.MiscellaneousTransactionAF == AF)
                            .ToList().OrderBy(i => i.CreatedUtcTime);
        }

        public Invoice GetByIV(string IV)
        {
            return MemorialContext.Invoices.Where(i => i.IV == IV).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}