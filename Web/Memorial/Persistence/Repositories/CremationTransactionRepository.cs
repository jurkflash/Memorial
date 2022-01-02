using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CremationTransactionRepository : Repository<CremationTransaction>, ICremationTransactionRepository
    {
        public CremationTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public CremationTransaction GetActive(string AF)
        {
            return MemorialContext.CremationTransactions
                .Where(ct => ct.AF == AF)                                            
                .Include(ct => ct.FuneralCompany)
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.CremationItem.Cremation)
                .Include(ct => ct.CremationItem.Cremation.Site)
                .SingleOrDefault();
        }

        public IEnumerable<CremationTransaction> GetByItem(int itemId, string filter)
        {
            var transactions = MemorialContext.CremationTransactions
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.Applicant)
                .Include(ct => ct.Deceased)
                .Include(ct => ct.FuneralCompany)
                .Where(ct => ct.CremationItemId == itemId);

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(ct => ct.AF.Contains(filter) || ct.Applicant.Name.Contains(filter) || (ct.Applicant.Name2 != null && ct.Applicant.Name2.Contains(filter))).ToList();
            }
        }

        public IEnumerable<CremationTransaction> GetByItemAndDeceased(int itemId, int deceasedId)
        {
            return MemorialContext.CremationTransactions
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.Applicant)
                .Include(ct => ct.Deceased)
                .Include(ct => ct.FuneralCompany)
                .Where(ct => ct.CremationItemId == itemId 
                    && ct.DeceasedId == deceasedId).ToList();
        }

        public IEnumerable<CremationTransaction> GetByApplicant(int id)
        {
            return MemorialContext.CremationTransactions
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.Applicant)
                .Include(ct => ct.Deceased)
                .Where(ct => ct.ApplicantId == id).ToList();
        }

        public IEnumerable<CremationTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.CremationTransactions
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.Applicant)
                .Include(ct => ct.Deceased)
                .Where(ct => ct.ApplicantId == applicantId
                                            && ct.CremationItemId == itemId).ToList();
        }

        public IEnumerable<CremationTransaction> GetRecent(int number, int siteId)
        {
            return MemorialContext.CremationTransactions
                .Where(t => t.CremationItem.Cremation.SiteId == siteId)
                .Include(t => t.Applicant)
                .Include(t => t.CremationItem)
                .Include(t => t.CremationItem.Cremation)
                .Include(t => t.CremationItem.SubProductService)
                .Include(t => t.CremationItem.SubProductService.Product)
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