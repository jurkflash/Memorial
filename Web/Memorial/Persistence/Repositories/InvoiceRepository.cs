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
            return MemorialContext.Invoices.Where(i => i.AncestralTabletTransactionAF == AF && i.DeleteDate == null)
                .ToList().OrderBy(i => i.CreatedDate);
        }

        public IEnumerable<Invoice> GetByActiveCremationAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.CremationTransactionAF == AF && i.DeleteDate == null)
                            .ToList().OrderBy(i => i.CreatedDate);
        }

        public IEnumerable<Invoice> GetByActiveCemeteryAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.CemeteryTransactionAF == AF && i.DeleteDate == null)
                            .ToList().OrderBy(i => i.CreatedDate);
        }

        public IEnumerable<Invoice> GetByActiveSpaceAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.SpaceTransactionAF == AF && i.DeleteDate == null)
                            .ToList().OrderBy(i => i.CreatedDate);
        }

        public IEnumerable<Invoice> GetByActiveUrnAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.UrnTransactionAF == AF && i.DeleteDate == null)
                            .ToList().OrderBy(i => i.CreatedDate);
        }

        public IEnumerable<Invoice> GetByActiveColumbariumAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.ColumbariumTransactionAF == AF && i.DeleteDate == null)
                            .ToList().OrderBy(i => i.CreatedDate);
        }

        public IEnumerable<Invoice> GetByActiveMiscellaneousAF(string AF)
        {
            return MemorialContext.Invoices.Where(i => i.MiscellaneousTransactionAF == AF && i.DeleteDate == null)
                            .ToList().OrderBy(i => i.CreatedDate);
        }

        public Invoice GetByActiveIV(string IV)
        {
            return MemorialContext.Invoices.Where(i => i.IV == IV && i.DeleteDate == null).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}