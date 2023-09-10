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

        public IEnumerable<Receipt> GetByAncestralTabletAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.AncestralTabletTransactionAF == AF)
                .Include(r => r.PaymentMethod)
                .ToList()
                .OrderBy(r => r.CreatedUtcTime);
        }

        public IEnumerable<Receipt> GetByCremationAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.CremationTransactionAF == AF)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedUtcTime);
        }

        public IEnumerable<Receipt> GetByCemeteryAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.CemeteryTransactionAF == AF)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedUtcTime);
        }

        public IEnumerable<Receipt> GetBySpaceAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.SpaceTransactionAF == AF)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedUtcTime);
        }

        public IEnumerable<Receipt> GetByUrnAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.UrnTransactionAF == AF)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedUtcTime);
        }

        public IEnumerable<Receipt> GetByColumbariumAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.ColumbariumTransactionAF == AF)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedUtcTime);
        }

        public IEnumerable<Receipt> GetByMiscellaneousAF(string AF)
        {
            return MemorialContext.Receipts
                .Where(r => r.MiscellaneousTransactionAF == AF)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedUtcTime);
        }

        public float GetTotalAmountByAncestralTabletAF(string AF)
        {
            return MemorialContext.Receipts.Where(r => r.AncestralTabletTransactionAF == AF).Select(r => r.Amount).DefaultIfEmpty(0).Sum();
        }

        public float GetTotalAmountByCremationAF(string AF)
        {
            return MemorialContext.Receipts.Where(r => r.CremationTransactionAF == AF).Select(r => r.Amount).DefaultIfEmpty(0).Sum();
        }

        public float GetTotalAmountByCemeteryAF(string AF)
        {
            return MemorialContext.Receipts.Where(r => r.CemeteryTransactionAF == AF).Select(r => r.Amount).DefaultIfEmpty(0).Sum();
        }

        public float GetTotalAmountBySpaceAF(string AF)
        {
            return MemorialContext.Receipts.Where(r => r.SpaceTransactionAF == AF).Select(r => r.Amount).DefaultIfEmpty(0).Sum();
        }

        public float GetTotalAmountByUrnAF(string AF)
        {
            return MemorialContext.Receipts.Where(r => r.UrnTransactionAF == AF).Select(r => r.Amount).DefaultIfEmpty(0).Sum();
        }

        public float GetTotalAmountByColumbariumAF(string AF)
        {
            return MemorialContext.Receipts.Where(r => r.ColumbariumTransactionAF == AF).Select(r => r.Amount).DefaultIfEmpty(0).Sum();
        }

        public float GetTotalAmountByMiscellaneousAF(string AF)
        {
            return MemorialContext.Receipts.Where(r => r.MiscellaneousTransactionAF == AF).Select(r => r.Amount).DefaultIfEmpty(0).Sum();
        }

        public IEnumerable<Receipt> GetByIV(string IV)
        {
            return MemorialContext.Receipts
                .Where(r => r.InvoiceIV == IV)
                .Include(r => r.PaymentMethod)
                .ToList().OrderBy(r => r.CreatedUtcTime);
        }

        public Receipt GetByRE(string RE)
        {
            return MemorialContext.Receipts
                .Where(r => r.RE == RE)
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