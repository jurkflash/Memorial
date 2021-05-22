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
                    r.DeleteDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreateDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveCremationAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.CremationTransactionAF == AF &&
                    r.DeleteDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreateDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveCemeteryAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.CemeteryTransactionAF == AF &&
                    r.DeleteDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreateDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveSpaceAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.SpaceTransactionAF == AF &&
                    r.DeleteDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreateDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveUrnAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.UrnTransactionAF == AF &&
                    r.DeleteDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreateDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveColumbariumAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.ColumbariumTransactionAF == AF &&
                    r.DeleteDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreateDate);
        }

        public IEnumerable<Receipt> GetByNonOrderActiveMiscellaneousAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.MiscellaneousTransactionAF == AF &&
                    r.DeleteDate == null &&
                    r.InvoiceIV == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreateDate);
        }

        public IEnumerable<Receipt> GetByActiveIV(string IV)
        {
            return MemorialContext.Receipts
                .Where(r => r.InvoiceIV == IV && r.DeleteDate == null)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreateDate);
        }

        public Receipt GetByActiveRE(string RE)
        {
            return MemorialContext.Receipts.Where(r => r.RE == RE && r.DeleteDate == null)
                            .SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}