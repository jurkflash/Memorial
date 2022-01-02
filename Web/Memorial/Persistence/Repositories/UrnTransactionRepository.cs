using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class UrnTransactionRepository : Repository<UrnTransaction>, IUrnTransactionRepository
    {
        public UrnTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public UrnTransaction GetActive(string AF)
        {
            return MemorialContext.UrnTransactions
                .Include(ut => ut.UrnItem)
                .Include(ut => ut.UrnItem.Urn)
                .Include(ut => ut.UrnItem.Urn.Site)
                .Include(ut => ut.Applicant)
                .Where(ut => ut.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<UrnTransaction> GetByItem(int itemId, string filter)
        {
            var transactions = MemorialContext.UrnTransactions
                .Include(ut => ut.UrnItem)
                .Include(ut => ut.UrnItem.Urn)
                .Include(ut => ut.UrnItem.Urn.Site)
                .Include(ut => ut.Applicant)
                .Where(ut => ut.UrnItemId == itemId).ToList();

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(ut => ut.AF.Contains(filter) || ut.Applicant.Name.Contains(filter) || (ut.Applicant.Name2 != null && ut.Applicant.Name2.Contains(filter))).ToList();
            }
        }

        public IEnumerable<UrnTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.UrnTransactions
                .Include(ut => ut.UrnItem)
                .Include(ut => ut.UrnItem.Urn)
                .Include(ut => ut.Applicant)
                .Where(ut => ut.ApplicantId == applicantId
                                            && ut.UrnItemId == itemId).ToList();
        }

        public IEnumerable<UrnTransaction> GetRecent(int number, int siteId)
        {
            return MemorialContext.UrnTransactions
                .Where(t => t.DeletedDate == null && t.UrnItem.Urn.SiteId == siteId)
                .Include(t => t.Applicant)
                .Include(t => t.UrnItem)
                .Include(t => t.UrnItem.Urn)
                .Include(t => t.UrnItem.SubProductService)
                .Include(t => t.UrnItem.SubProductService.Product)
                .OrderByDescending(t => t.CreatedDate)
                .Take(number)
                .ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}