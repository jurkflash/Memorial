using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class ReceiptRepository : Repository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<Receipt> GetByNonOrderActiveAncestralTabletAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.AncestralTabletTransactionAF == AF &&
                    r.DeletedDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveCremationAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.CremationTransactionAF == AF &&
                    r.DeletedDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveCemeteryAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.CemeteryTransactionAF == AF &&
                    r.DeletedDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveSpaceAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.SpaceTransactionAF == AF &&
                    r.DeletedDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveUrnAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.UrnTransactionAF == AF &&
                    r.DeletedDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveColumbariumAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.ColumbariumTransactionAF == AF &&
                    r.DeletedDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveMiscellaneousAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.MiscellaneousTransactionAF == AF &&
                    r.DeletedDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedDate);
        }

        public IEnumerable<Receipt> GetByActiveIV(string IV)
        {
            return MemorialContext.Receipts
                .Where(r => r.InvoiceIV == IV && r.DeletedDate == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedDate);
        }

        public Receipt GetByActiveRE(string RE)
        {
            return MemorialContext.Receipts
                .Where(r => r.RE == RE && r.DeletedDate == null)
                .Include(r => r.Invoice)
                .Include(r => r.PaymentMethod)
                .SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}